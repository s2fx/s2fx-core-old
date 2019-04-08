using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata.Types;

namespace S2fx.Model.Annotations {


    public class XmlObjectFieldAttribute : AbstractFieldAttribute {
        public override string FieldTypeName => BuiltinFieldTypeNames.XmlObjectTypeName;
    }

}
