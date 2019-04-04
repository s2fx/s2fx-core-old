using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using S2fx.Model.Annotations;
using S2fx.Utility;
using System.ComponentModel;

namespace S2fx.Model.Metadata.Types {

    public interface IPrimitiveFieldType : IFieldType {
        Type ClrType { get; }
    }

    public abstract class AbstractPrimitiveFieldType : AbstractFieldType, IPrimitiveFieldType {

        public abstract Type ClrType { get; }

        public override MetaField LoadClrProperty(PropertyInfo propertyInfo) {
            var pt = propertyInfo.PropertyType;
            if (pt.IsGenericType) {
                if (pt.GetGenericTypeDefinition() == typeof(Nullable<>)
                    && pt.GetGenericArguments().First() != this.ClrType) {
                    throw new ArgumentOutOfRangeException(nameof(propertyInfo));
                }
            }

            var attrs = propertyInfo.GetCustomAttributes();
            var primitiveAttr = propertyInfo.GetCustomAttribute<PrimitiveFieldAttribute>();
            return new PrimitiveMetaField() {
                Name = propertyInfo.Name,
                DisplayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? propertyInfo.Name,
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
            };

        }
    }

}
