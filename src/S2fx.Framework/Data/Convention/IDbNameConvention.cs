using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Convention {

    public interface IDbNameConvention {
        string GetTableName(string moduleName, string entityTypeName);
        string GetColumnName(string propertyName);
        string MakeDbObjectFullName(string moduleName, string viewName);
    }

    public static class IDbNameConventionExtensions {
        public static string GetTableName(this IDbNameConvention self, string moduleName, Type entityType) =>
            self.GetTableName(moduleName, entityType.Name);
    }
}
