using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace S2fx.Model.Metadata.Types {

    public abstract class AbstractPropertyType : IPropertyType {

        public abstract string Name { get; }

        public abstract MetaProperty LoadClrProperty(PropertyInfo propertyInfo);

        public abstract bool TryParsePropertyValue(
            MetaProperty property, string value, out object result, string format = null);
    }

}
