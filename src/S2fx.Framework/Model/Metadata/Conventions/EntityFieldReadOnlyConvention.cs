using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using S2fx.Model.Metadata;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Conventions {

    public class EntityFieldReadOnlyConvention : IMetadataConvention<MetaField> {

        public void Apply(MetaField field) {
            field.IsReadOnly = field.ClrPropertyInfo.GetCustomAttribute<ReadOnlyAttribute>() != null;
        }
    }

}
