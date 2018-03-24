using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using S2fx.Model.Metadata;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Conventions {

    public class EntityPropertyMaxLengthConvention : IMetadataConvention<MetaProperty> {

        public void Apply(MetaProperty property) {
            var maxLengthAttr = property.ClrPropertyInfo.GetCustomAttribute<MaxLengthAttribute>();
            property.MaxLength = maxLengthAttr?.Length ?? -1;
        }
    }

}
