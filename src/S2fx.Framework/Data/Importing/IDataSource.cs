using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Data.Importing.Model;

namespace S2fx.Data.Importing {

    public interface IDataSource {
        string Kind { get; }

        IEnumerable<object> GetAllRows(DataSourceInfo dataSourceInfo);

        Func<object, object> BindInputPropertyGetter(string fromExpression);
    }

}
