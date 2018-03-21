using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using S2fx.Model.Annotations;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Types {

    public interface IPrimitivePropertyType : IPropertyType {
        Type ClrType { get; }
    }

    public abstract class AbstractPrimitivePropertyType : AbstractPropertyType, IPrimitivePropertyType {

        public abstract Type ClrType { get; }

        public override MetaProperty LoadClrProperty(PropertyInfo propertyInfo) {
            var pt = propertyInfo.PropertyType;
            if (pt.IsGenericType) {
                if (pt.GetGenericTypeDefinition() == typeof(Nullable<>)
                    && pt.GetGenericArguments().First() != this.ClrType) {
                    throw new ArgumentOutOfRangeException(nameof(propertyInfo));
                }
            }

            var attrs = propertyInfo.GetCustomAttributes();
            var primitiveAttr = propertyInfo.GetCustomAttribute<PrimitivePropertyAttribute>();
            var requiredAttr = propertyInfo.GetCustomAttribute<RequiredAttribute>();
            var isNullableType = propertyInfo.PropertyType.IsNullableType();
            return new PrimitiveMetaProperty() {
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
                Length = primitiveAttr != null ? primitiveAttr.Length : -1,
                Name = propertyInfo.Name,
                IsRequired = requiredAttr != null ? true : !isNullableType,
            };

        }
    }

}
