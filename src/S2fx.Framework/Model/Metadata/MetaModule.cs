using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public class MetaModule : AnyMetadata {

        public override void AcceptVisitor(IModelMetadataVisitor visitor) =>
            visitor.VisitModule(this);

    }

}
