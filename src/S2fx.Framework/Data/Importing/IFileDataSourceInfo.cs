using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Importing {

    public interface IFileDataSourceInfo : IDataSourceInfo {
        string Path { get; }
    }

}
