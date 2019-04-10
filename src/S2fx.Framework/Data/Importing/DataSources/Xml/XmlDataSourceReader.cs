using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Data.Importing {

    public class XmlDataSourceReader : IDataSourceReader {

        public IReadOnlyDictionary<string, string> Result => throw new NotImplementedException();

        public void Dispose() {
            throw new NotImplementedException();
        }

        public Task<bool> ReadAsync() {
            throw new NotImplementedException();
        }
    }

}
