using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public class MetadataModel : IMetadataNode {

        public Dictionary<string, MetaEntity> Entities { get; } = new Dictionary<string, MetaEntity>();

        public void AcceptVisitor(IMetadataVisitor visitor) {
            visitor.VisitModel(this);
        }

    }

}
