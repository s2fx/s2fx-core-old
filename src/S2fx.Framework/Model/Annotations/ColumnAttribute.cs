using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ColumnAttribute : Attribute {
        public string Name { get; }

        public ColumnAttribute(string name) {
            this.Name = name;
        }
    }
}
