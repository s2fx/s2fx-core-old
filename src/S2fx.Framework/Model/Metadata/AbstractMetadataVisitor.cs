using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public abstract class AbstractMetadataVisitor : IMetadataVisitor {

        public void VisitAny(AnyMetadata any) => any.AcceptVisitor(this);

        public virtual void VisitModel(MetadataModel model) {
        }

        public virtual void VisitEntity(MetaEntity entity) {
        }

        public virtual void VisitField(MetaField property) {
        }
    }

}
