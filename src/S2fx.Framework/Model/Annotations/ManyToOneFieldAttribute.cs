using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata.Types;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ManyToOneFieldAttribute : AbstractRelationalFieldAttribute {
        public override string FieldTypeName => BuiltinFieldTypeNames.ManyToOneTypeName;

        public ManyToOneFieldAttribute(string refEntity = null, string mappedBy = null) : base(refEntity, mappedBy) {
        }
    }

}
