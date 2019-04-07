using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Conventions;
using S2fx.Environment.Extensions.Entity;
using S2fx.Model.Metadata;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Conventions {

    public class EntityDbNameConvention : AbstractEntityConvention {
        private readonly IDbNameConvention _dbNameConvention;

        public EntityDbNameConvention(IDbNameConvention dbNameConvention) {
            this._dbNameConvention = dbNameConvention;
        }

        public override void Apply(MetaEntity entity) {
            entity.DbName = _dbNameConvention.EntityToTable(entity.Name);
        }

    }

}
