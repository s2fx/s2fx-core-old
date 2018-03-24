using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Conventions {

    public class EntityPropertyDbNameConvention : IMetadataConvention<MetaProperty> {

        public void Apply(MetaProperty property) {
            property.DbName = property.Name.ToSnakeCase();
        }
    }

}
