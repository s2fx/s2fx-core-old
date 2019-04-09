using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using S2fx.Model.Metadata.Types;

namespace S2fx.Model.Metadata.Conventions.Entity {


    public class EntityManyToOneFieldsDependencyConvention : AbstractEntityConvention {

        public EntityManyToOneFieldsDependencyConvention() {
        }

        public override void Apply(MetaEntity entity) {
            var m2oFields = entity.Fields.Values
                .Where(x => x.Type is ManyToOneFieldType)
                .Select(x => (ManyToOneMetaField)x);
            foreach (var f in m2oFields) {
                if (!entity.DependentEntities.Contains(f.RefEntityName)) {
                    entity.DependentEntities.Add(f.RefEntityName);
                }
            }

        }

    }

}
