using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace S2fx.Data.Convention {

    public class S2DbNameConvention : IDbNameConvention {

        public string EntityToTable(string entityFullName) =>
            ToSnakeCase(entityFullName.Replace(".", ""));

        public string EntityPropertyToColumn(string propertyName) =>
            ToSnakeCase(propertyName);


        public string MakeDbObjectFullName(string moduleName, string objName) {
            var fullName = moduleName + '.' + objName;
            return ToSnakeCase(fullName);
        }

        public string DefaultJoinTableName(string entityName1, string propertyName1, string entityName2, string propertyName2) {
            var tuples = new[] { (entityName1, propertyName1), (entityName2, propertyName2) };
            var sortedTuples = tuples.OrderBy(t => t.Item1);
            var t1 = sortedTuples.ElementAt(0);
            var t2 = sortedTuples.ElementAt(1);
            return $"ref_{t1.Item1}_{t1.Item2}__{t2.Item1}_{t2.Item2}";
        }

        public string EntityClrTypeNameToEntity(string moduleName, string typeName) =>
            moduleName + '.' + (typeName.EndsWith("Entity") ? typeName.Substring(0, typeName.Length - "Entity".Length) : typeName);

        private static string ToSnakeCase(string input) {
            if (string.IsNullOrEmpty(input)) {
                return input;
            }
            return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()))
                .ToLowerInvariant();
        }

    }
}
