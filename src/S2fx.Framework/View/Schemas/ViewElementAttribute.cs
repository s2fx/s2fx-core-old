using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.View.Schemas {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ViewElementAttribute : Attribute, IViewElementMetadata {
        public string TypeName { get; }

        public ViewElementAttribute(string typeName) {
            this.TypeName = typeName;
        }
    }

}
