using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using S2fx.Utility;

namespace S2fx.Conventions {

    public class S2DbNameConvention : IDbNameConvention {

        public string EntityToTable(string entityFullName) =>
            entityFullName.Replace(".", "").ToSnakeCase();

        public string EntityPropertyToColumn(string propertyName) =>
            propertyName.ToSnakeCase();


        public string MakeDbObjectFullName(string moduleName, string objName) {
            var fullName = moduleName + '.' + objName;
            return fullName.ToSnakeCase();
        }

        public string EntityClrTypeNameToEntity(string moduleName, string typeName) =>
            moduleName + '.' + (typeName.EndsWith("Entity") ? typeName.Substring(0, typeName.Length - "Entity".Length) : typeName);


    }
}
