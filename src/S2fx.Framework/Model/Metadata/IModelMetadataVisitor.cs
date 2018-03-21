using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public interface IModelMetadataVisitor {

        void VisitAny(AnyMetadata any);

        void VisitModel(MetaModel model);

        void VisitEntity(MetaEntity property);

        void VisitProperty(MetaProperty property);

        void VisitModule(MetaModule module);
    }

}
