using System;
using System.Collections.Generic;
using System.Text;
using S2fx.View.Schemas;

namespace S2fx.View.Annotations {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ViewElementAttribute : Attribute, IViewElementMetadata {
        public string TypeName { get; }

        public ViewElementAttribute(string typeName) {
            this.TypeName = typeName;
        }
    }

}
