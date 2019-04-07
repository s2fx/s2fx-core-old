using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Conventions;
using S2fx.Model.Metadata;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Conventions {

    public class EntityFieldDbNameConvention : AbstractEntityFieldConvention {
        private readonly IDbNameConvention _dbNameConvention;

        public EntityFieldDbNameConvention(IDbNameConvention dbNameConvention) {
            this._dbNameConvention = dbNameConvention;
        }

        public override void Apply(MetaField field) {
            field.DbName = _dbNameConvention.EntityToTable(field.Name);
        }
    }

}
