using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Data.Importing.DataSources {

    public class CsvDataSourceReader : IDataSourceReader {

        readonly CsvHelper.CsvReader _csvHelperReader;

        public CsvDataSourceReader(TextReader tr) {
            // TODO FIXME
            _csvHelperReader = new CsvHelper.CsvReader(tr, System.Globalization.CultureInfo.InvariantCulture);
        }

        public async Task Initialize() {
            await _csvHelperReader.ReadAsync();
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
