using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public abstract class AbstractMetadata {
        public IEnumerable<Attribute> Attributes { get; set; }
    }

}
