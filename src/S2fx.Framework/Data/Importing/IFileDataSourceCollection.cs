using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Importing {

    public interface IFileDataSourceCollection : 
        IList<IFileDataSourceInfo>, 
        ICollection<IFileDataSourceInfo>, 
        IEnumerable<IFileDataSourceInfo>, 
        IEnumerable {
    }
}
