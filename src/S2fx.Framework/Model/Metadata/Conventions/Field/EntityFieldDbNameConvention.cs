using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Conventions {

    public class EntityFieldDbNameConvention : AbstractEntityFieldConvention {

        public override void Apply(MetaField field) {
            field.DbName = field.Name.ToSnakeCase();
        }
    }

}
