using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class DbIndexAttribute : Attribute {
        public string Name { get; }
        public string Fields { get; }

        public DbIndexAttribute(string fields, string name = null) {
            this.Name = name;
            this.Fields = fields;
        }

    }
}
