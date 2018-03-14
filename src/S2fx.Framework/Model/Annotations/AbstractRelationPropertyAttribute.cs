using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Annotations {

    public abstract class AbstractRelationPropertyAttribute : AbstractPropertyAttribute {
        public CascadeMethod Cascade { get; set; } = CascadeMethod.All;
    }
}
