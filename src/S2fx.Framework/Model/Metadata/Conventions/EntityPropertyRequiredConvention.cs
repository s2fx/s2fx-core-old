using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using S2fx.Model.Metadata;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Conventions {

    public class EntityPropertyRequiredConvention : IMetadataConvention<MetaProperty> {

        public void Apply(MetaProperty property) {
            if (property is IMetaPropertyWithIsRequired withIsRequired) {
                var clrType = property.ClrPropertyInfo.PropertyType;
                withIsRequired.IsRequired =
                    property.ClrPropertyInfo.GetCustomAttribute<ReadOnlyAttribute>() != null ? true : clrType.IsValueType;
            }
        }
    }

}
