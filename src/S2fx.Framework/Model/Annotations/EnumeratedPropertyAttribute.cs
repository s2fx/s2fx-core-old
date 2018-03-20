using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;

namespace S2fx.Model.Annotations {

    public class EnumeratedPropertyAttribute : AbstractPropertyAttribute {
        public override string PropertyTypeName => "Enumerated";
    }

}
