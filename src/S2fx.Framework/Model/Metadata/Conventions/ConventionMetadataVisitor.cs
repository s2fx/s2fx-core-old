using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata.Conventions {

    public class ConventionMetadataVisitor : AbstractMetadataVisitor {

        private readonly IEnumerable<IMetadataConvention<MetaEntity>> _entityConventions;
        private readonly IEnumerable<IMetadataConvention<MetaField>> _propertyConventions;

        public ConventionMetadataVisitor(
            IEnumerable<IMetadataConvention<MetaEntity>> entityConventions,
            IEnumerable<IMetadataConvention<MetaField>> propertyConventions) {
            _entityConventions = entityConventions;
            _propertyConventions = propertyConventions;
        }

        public override void VisitEntity(MetaEntity entity) {
            foreach (var convention in _entityConventions) {
                convention.Apply(entity);
            }

            foreach (var property in entity.Fields.Values) {
                property.AcceptVisitor(this);
            }
        }

        public override void VisitField(MetaField property) {
            foreach (var convention in _propertyConventions) {
                convention.Apply(property);
            }
        }

    }

}
