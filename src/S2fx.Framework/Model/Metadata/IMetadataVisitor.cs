using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public interface IMetadataVisitor {

        void VisitAny(AnyMetadata any);

        void VisitEntity(MetaEntity entity);

        void VisitProperty(MetaProperty property);
    }

}
