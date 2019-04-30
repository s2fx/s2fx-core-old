using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using S2fx.Data.Importing.Model;

namespace S2fx.Data.Importing.DataSources {

    public interface IDataSource {
        string Format { get; }

        IDataSourceReader Open(Stream stream, string selector);
    }

}
