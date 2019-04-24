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

    public class CsvDataSource : IDataSource {
        public const string CsvFormat = "CSV";

        public string Format => CsvFormat;

        public IEnumerable<object> GetAllRows(Stream stream, string selector) {
            throw new NotImplementedException();
        }

        public IDataSourceReader Open(Stream stream, string selector) {
            return new CsvDataSourceReader(new StreamReader(stream));
        }
    }

}
