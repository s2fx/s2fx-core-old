using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Data.Importing.DataSources {

    public class CsvDataSourceReader : IDataSourceReader {

        readonly CsvHelper.CsvReader _csvHelperReader;

        public CsvDataSourceReader(TextReader tr) {
            _csvHelperReader = new CsvHelper.CsvReader(tr, false);
        }

        public async Task Initialize() {
            await Task.Run(_csvHelperReader.ReadHeader);
        }

        public void Dispose() {
            _csvHelperReader.Dispose();
        }

        public object GetField(string name) =>
            _csvHelperReader.GetField(name);

        public async Task<bool> ReadAsync() {
            return await _csvHelperReader.ReadAsync();
        }

    }

}
