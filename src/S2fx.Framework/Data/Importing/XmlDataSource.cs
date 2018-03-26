using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using S2fx.Data.Importing.Model;
using System.Linq.Expressions;

namespace S2fx.Data.Importing {

    public class XmlDataSource : IDataSource {
        public const string XmlKind = "Xml";
        public const string XmlDocument = "XmlDocument";
        public const string BindElementsXPath = "BindElementsXPath";

        public string Kind => XmlKind;

        public IEnumerable<object> GetAllRows(DataSourceInfo dataSourceInfo) {
            var xdoc = dataSourceInfo.Properties[XmlDocument] as XDocument;
            var rows = xdoc.XPathSelectElements(BindElementsXPath);
            return rows;
        }

        public Func<object, object> BindInputPropertyGetter(string fromExpression) {
            return new Func<object, object>((object row) => {
                var element = (XElement)row;
                return element.XPathSelectElement(fromExpression);
            });
        }
    }

}
