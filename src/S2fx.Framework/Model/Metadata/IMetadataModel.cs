using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public class MetadataModel : IMetadataNode {

        public IList<MetaEntity> Entities { get; } = new List<MetaEntity>();

        public void AcceptVisitor(IMetadataVisitor visitor) {
            visitor.VisitModel(this);
            foreach (var entity in this.Entities) {
                visitor.VisitEntity(entity);
            }
        }

    }

}
