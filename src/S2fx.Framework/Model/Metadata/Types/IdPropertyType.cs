using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace S2fx.Model.Metadata.Types {

    public interface IIdPropertyType : IPropertyType {
        Type ClrType { get; }
    }

    public class IdPropertyType : IIdPropertyType {

        public string Name => "Id";

        public Type ClrType => typeof(long);

        public MetaProperty ToMetaProperty(PropertyInfo propertyInfo) {
            return new IdMetaProperty() {
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
                Length = -1,
                Name = propertyInfo.Name,
            };
        }
    }

}
