using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public abstract class AbstractMetaEntityAnnotation : AnyMetadata {
        public abstract string Name { get; }
    }

}
