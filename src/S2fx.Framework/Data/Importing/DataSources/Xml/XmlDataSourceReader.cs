using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace S2fx.Data.Importing.DataSources {

    public class XmlDataSourceReader : IDataSourceReader {
        readonly Stream _stream;
        readonly string _selector;
        readonly Dictionary<string, Func<XNode, string>> _columnValueGetters;
        XDocument _xdoc;
        XNode[] _rows;
        int _currentRowIndex = -1;


        public XmlDataSourceReader(Stream stream, string selector) {
            _stream = stream;
            _selector = selector;
            _columnValueGetters = new Dictionary<string, Func<XNode, string>>();
        }

        public async Task Initialize() {
            await Task.Run(() => {
                _xdoc = XDocument.Load(_stream);
                _rows = ((IEnumerable)_xdoc.XPathEvaluate(_selector)).OfType<XNode>().ToArray();
            });
            _currentRowIndex = -1;
        }

        public void Dispose() {
            _stream.Dispose();
        }

        public object GetField(string expression) {
            if (_currentRowIndex >= _rows.Length) {
                throw new IndexOutOfRangeException();
            }

            Func<XNode, string> getter = null;
            if (!_columnValueGetters.TryGetValue(expression, out getter)) {
                getter = this.CreateFieldValueTextMapping(expression);
                _columnValueGetters.Add(expression, getter);
            }
            var row = _rows[_currentRowIndex];
            var propertyValue = getter(row);
            return propertyValue;
        }

        public Task<bool> ReadAsync() {
            _currentRowIndex++;
            if (_currentRowIndex < _rows.Length) {
                return Task.FromResult(true);
            }
            else {
                return Task.FromResult(false);
            }
        }

        Func<XNode, string> CreateFieldValueTextMapping(string fromExpression) {
            return new Func<XNode, string>((XNode row) => {
                var recordNode = row;
                var propertyObject = ((IEnumerable)recordNode.XPathEvaluate(fromExpression))
                    .OfType<XObject>()
                    .Single();
                if (propertyObject.NodeType == XmlNodeType.Attribute) {
                    return ((XAttribute)propertyObject).Value;
                }
                else if (propertyObject.NodeType == XmlNodeType.Element) {
                    return ((XElement)propertyObject).Value;
                }
                else {
                    throw new NotSupportedException();
                }
            });
        }

    }

}
