using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using S2fx.Data.Importing.Model;
using System.Linq.Expressions;

namespace S2fx.Data.Importing.DataSources {

    public class XmlDataSource : IDataSource {
        public const string XmlFormat = "XML";

        public string Format => XmlFormat;

        public IEnumerable<object> GetAllRows(Stream stream, string selector) {
            var xdoc = XDocument.Load(stream);
            var rows = ((IEnumerable<object>)xdoc.XPathEvaluate(selector)).OfType<XNode>();
            return rows;
        }

        public Func<object, string> CreateFieldValueTextMapping(string fromExpression) {
            return new Func<object, string>((object row) => {
                var recordNode = (XNode)row;
                var propertyObject = ((IEnumerable<object>)recordNode.XPathEvaluate(fromExpression))
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

        public IDataSourceReader Open(Stream stream, string selector) {
            return new XmlDataSourceReader(stream, selector);
        }
    }

}
