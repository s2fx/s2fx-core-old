using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Conventions {

    public interface IDbNameConvention {
        string EntityToTable(string entityFullName);
        string EntityFieldToColumn(string propertyName);
        string MakeDbObjectFullName(string moduleName, string viewName);
        string EntityClrTypeNameToEntity(string moduleName, string typeName);
    }
}
