using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace S2fx.Model.Metadata {

    public interface IFieldType : IPropertyValueConverter {
        string Name { get; }
        bool SelectDefaultValue { get; }
        bool UniqueDefaultValue { get; }
        MetaField LoadClrProperty(PropertyInfo propertyInfo);
    }


}
