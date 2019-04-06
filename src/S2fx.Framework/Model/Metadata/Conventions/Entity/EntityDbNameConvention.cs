using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Environment.Extensions.Entity;
using S2fx.Model.Metadata;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Conventions {

    public class EntityDbNameConvention : AbstractEntityConvention {

        public override void Apply(MetaEntity entity) {
            entity.DbName = entity.Name.Replace(".", "").ToSnakeCase();
        }

    }

}
