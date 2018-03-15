using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Convention {

    public interface IDbNameConvention {
        string EntityToTable(string moduleName, string entityName);
        string EntityPropertyToColumn(string propertyName);
        string MakeDbObjectFullName(string moduleName, string viewName);
    }
}
