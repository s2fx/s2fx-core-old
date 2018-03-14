using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public abstract class AbstractMetadataDescriptor {
        IEnumerable<Attribute> Attributes { get; set; }
    }

}
