using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using S2fx.Model.Annotations;
using S2fx.Model.Metadata;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Conventions {

    public class EntityFieldUniqueConvention : IMetadataConvention<MetaField> {

        public void Apply(MetaField field) {
            field.IsUnique = field.ClrPropertyInfo.GetCustomAttribute<UniqueAttribute>() != null;
        }
    }

}
