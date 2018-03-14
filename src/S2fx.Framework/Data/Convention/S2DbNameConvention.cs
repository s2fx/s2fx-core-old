using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Convention {

    public class S2DbNameConvention : IDbNameConvention {

        public string GetColumnName(string propertyName) =>
            propertyName.ToLowerInvariant().Replace('.', '_');

        public string GetTableName(string moduleName, string entityTypeName) =>
            this.MakeDbObjectFullName(moduleName, entityTypeName);

        public string MakeDbObjectFullName(string moduleName, string objName) {
            var fullName = moduleName + '_' + objName;
            return fullName.Replace('.', '_').ToLowerInvariant();
        }
    }
}
