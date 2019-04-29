using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Data.Importing.Schemas;

namespace S2fx.Data.Importing {

    public static class FileDataSourceCollectionExtensions {

        public static IFileDataSourceCollection AddXmlFile(this IFileDataSourceCollection self, string path) {
            self.Add(new XmlFileDataSource { Path = path });
            return self;
        }

        public static IFileDataSourceCollection AddCsvFile(this IFileDataSourceCollection self, string path) {
            self.Add(new CsvFileDataSource { Path = path });
            return self;
        }
    }

}
