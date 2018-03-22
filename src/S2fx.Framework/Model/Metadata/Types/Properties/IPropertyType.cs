using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace S2fx.Model.Metadata {

    public interface IPropertyType {
        string Name { get; }
        MetaProperty LoadClrProperty(PropertyInfo propertyInfo);
    }


}
