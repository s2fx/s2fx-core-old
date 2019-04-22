using System;
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
        int _currentRowIndex = 0;


        public XmlDataSourceReader(Stream stream, string selector) {
            _stream = stream;
            _selector = selector;
            _columnValueGetters = new Dictionary<string, Func<XNode, string>>();
        }

        public async Task Initialize() {
            await Task.Run(() => {
                _xdoc = XDocument.Load(_stream);
                _rows = ((IEnumerable<XNode>)_xdoc.XPathEvaluate(_selector)).OfType<XNode>().ToArray();
            });
            _currentRowIndex = 0;
        }

        public void Dispose() {
            _stream.Dispose();
        }

        public object GetField(string expression) {
            Func<XNode, string> getter = null;
            if (!_columnValueGetters.TryGetValue(expression, out getter)) {
                _columnValueGetters.Add(expression, this.CreatePropertyValueTextGetter(expression));
            }
            var row = _rows[_currentRowIndex];
            return getter(row);
        }

        public Task<bool> ReadAsync() {
            if (_currentRowIndex < _rows.Length) {
                _currentRowIndex++;
                return Task.FromResult(true);
            }
            else {
                return Task.FromResult(false);
            }
        }

        Func<XNode, string> CreatePropertyValueTextGetter(string fromExpression) {
            return new Func<XNode, string>((XNode row) => {
                var recordNode = row;
                var propertyObject = ((IEnumerable<XNode>)recordNode.XPathEvaluate(fromExpression))
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
