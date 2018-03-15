using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace S2fx.Data.Convention {

    public class S2DbNameConvention : IDbNameConvention {

        public string EntityToTable(string moduleName, string entityName) =>
            this.MakeDbObjectFullName(moduleName, entityName);

        public string EntityPropertyToColumn(string propertyName) =>
            ToSnakeCase(propertyName);


        public string MakeDbObjectFullName(string moduleName, string objName) {
            var fullName = moduleName + '_' + objName;
            return fullName.Replace('.', '_').ToLowerInvariant();
        }

        private static string ToSnakeCase(string input) {
            if (string.IsNullOrEmpty(input)) {
                return input;
            }
            return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()))
                .ToLowerInvariant();
        }
    }
}
