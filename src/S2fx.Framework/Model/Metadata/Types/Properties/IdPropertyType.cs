using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace S2fx.Model.Metadata.Types {

    public interface IIdPropertyType : IPropertyType {
        Type ClrType { get; }
    }

    public class IdPropertyType : AbstractPropertyType, IIdPropertyType {

        public override string Name => "Id";

        public Type ClrType => typeof(long);

        public override MetaProperty LoadClrProperty(PropertyInfo propertyInfo) {
            return new IdMetaProperty() {
                Name = propertyInfo.Name,
                DisplayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? propertyInfo.Name,
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo
            };
        }

        public override bool TryParsePropertyValue(MetaProperty property, string value, out object result, string format = null) {
            var succeed = long.TryParse(value, out var typedValue);
            result = typedValue;
            return succeed;
        }
    }

}
