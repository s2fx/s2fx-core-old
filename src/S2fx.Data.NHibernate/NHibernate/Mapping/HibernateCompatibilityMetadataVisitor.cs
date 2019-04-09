using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;

namespace S2fx.Data.NHibernate.Mapping {

    public class HibernateCompatibilityMetadataVisitor : AbstractMetadataVisitor {

        public override void VisitEntity(MetaEntity entity) {
            base.VisitEntity(entity);
        }

        public override void VisitField(MetaField field) {
            base.VisitField(field);
        }

    }

}
