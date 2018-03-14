using System;
using S2fx.Model.Metadata;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TableAttribute : Attribute {
        public string Table { get; }

        public TableAttribute(string tableName) {
            this.Table = tableName;
        }

    }
}
