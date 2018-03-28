using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Importing.Model {

    public class PropertyBinder {
        public Func<object, string> SourceGetter { get; }
        public string TargetProperty { get; }

        public PropertyBinder(Func<object, string> sourceGetter, string targetProperty) {
            this.SourceGetter = sourceGetter;
            this.TargetProperty = targetProperty;

        }
    }

}
