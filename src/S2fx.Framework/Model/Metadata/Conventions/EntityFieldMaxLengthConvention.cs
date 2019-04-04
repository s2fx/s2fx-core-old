using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using S2fx.Model.Metadata;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Conventions {

    public class EntityFieldMaxLengthConvention : IMetadataConvention<MetaField> {

        public void Apply(MetaField field) {
            var maxLengthAttr = field.ClrPropertyInfo.GetCustomAttribute<MaxLengthAttribute>();
            field.MaxLength = maxLengthAttr?.Length ?? -1;
        }
    }

}
