using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Importing {

    public interface IFileDataSourceDescriptor : IDataSourceDescriptor {
        string Path { get; }
    }

}
