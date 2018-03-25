using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class WidgetAttribute : Attribute {
        public string Widget { get; }

        public WidgetAttribute(string widget) {
            this.Widget = widget;
        }
    }

}
