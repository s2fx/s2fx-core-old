using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Data.Importing {

    public interface IDataSourceReader : IDisposable {
        Task<bool> ReadAsync();
        IReadOnlyDictionary<string, string> Result { get; }
    }

}
