using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using S2fx.Model.Metadata;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Conventions {

    public class EntityPropertyReadOnlyConvention : IMetadataConvention<MetaProperty> {

        public void Apply(MetaProperty property) {
            property.IsReadOnly = property.ClrPropertyInfo.GetCustomAttribute<ReadOnlyAttribute>() != null;
        }
    }

}
