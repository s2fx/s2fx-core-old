using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using S2fx.Model.Metadata;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Conventions {

    public class EntityFieldRequiredConvention : IMetadataConvention<MetaField> {

        public void Apply(MetaField property) {
            if (property is IMetaFieldWithIsRequired withIsRequired) {
                var clrType = property.ClrPropertyInfo.PropertyType;
                withIsRequired.IsRequired =
                    property.ClrPropertyInfo.GetCustomAttribute<RequiredAttribute>() != null ? true : !clrType.IsNullableType();
            }
        }
    }

}
