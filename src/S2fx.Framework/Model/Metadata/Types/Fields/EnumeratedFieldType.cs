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

    public interface IEnumerableFieldType : IFieldType {
    }

    public class EnumerableFieldType : AbstractFieldType, IEnumerableFieldType {

        public override string Name => BuiltinFieldTypeNames.EnumerableTypeName;

        public override bool SelectDefaultValue => true;
        public override bool UniqueDefaultValue => false;
        public override bool LazyDefaultValue => false;

        public override MetaField LoadClrProperty(PropertyInfo propertyInfo) {

            var pt = propertyInfo.PropertyType;
            if (pt.IsGenericType) {
                if (pt.GetGenericTypeDefinition() == typeof(Nullable<>)
                    && pt.GetGenericArguments().First() != typeof(Enum)) {
                    throw new ArgumentOutOfRangeException(nameof(propertyInfo));
                }
            }

            var attrs = propertyInfo.GetCustomAttributes();
            var primitiveAttr = propertyInfo.GetCustomAttribute<EnumerableFieldAttribute>();
            return new EnumerableMetaField() {
                Name = propertyInfo.Name,
                DisplayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? propertyInfo.Name,
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
            };
        }

        public override bool TryParse(MetaField property, string value, out object result, string format = null) {
            var tryParseParams = new Type[] { typeof(string), property.ClrPropertyInfo.PropertyType.MakeByRefType() };
            var method = property.ClrPropertyInfo.PropertyType.GetMethod(nameof(Enum.TryParse), tryParseParams);
            var tryParseArgs = new object[] { value, null };
            var succeed = (bool)method.Invoke(null, tryParseArgs);
            result = tryParseArgs[1];
            return succeed;
        }
    }

}
