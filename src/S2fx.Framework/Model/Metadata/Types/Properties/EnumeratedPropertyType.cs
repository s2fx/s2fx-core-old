using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using S2fx.Model.Annotations;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Types {

    public interface IEnumerablePropertyType : IPropertyType {
    }

    public class EnumerablePropertyType : AbstractPropertyType, IEnumerablePropertyType {

        public override string Name => BuiltinPropertyTypeNames.EnumerableTypeName;

        public override MetaProperty LoadClrProperty(PropertyInfo propertyInfo) {

            var pt = propertyInfo.PropertyType;
            if (pt.IsGenericType) {
                if (pt.GetGenericTypeDefinition() == typeof(Nullable<>)
                    && pt.GetGenericArguments().First() != typeof(Enum)) {
                    throw new ArgumentOutOfRangeException(nameof(propertyInfo));
                }
            }

            var attrs = propertyInfo.GetCustomAttributes();
            var primitiveAttr = propertyInfo.GetCustomAttribute<EnumerablePropertyAttribute>();
            return new EnumerableMetaProperty() {
                Name = propertyInfo.Name,
                DisplayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? propertyInfo.Name,
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
            };
        }

        public override bool TryParsePropertyValue(MetaProperty property, string value, out object result, string format = null) {
            var tryParseParams = new Type[] { typeof(string), property.ClrPropertyInfo.PropertyType.MakeByRefType() };
            var method = property.ClrPropertyInfo.PropertyType.GetMethod(nameof(Enum.TryParse), tryParseParams);
            var tryParseArgs = new object[] { value, null };
            var succeed = (bool)method.Invoke(null, tryParseArgs);
            result = tryParseArgs[1];
            return succeed;
        }
    }

}
