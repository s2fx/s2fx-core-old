using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata.Types;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ManyToOnePropertyAttribute : AbstractRelationPropertyAttribute {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.ManyToOneTypeName;

        public ManyToOnePropertyAttribute(string refEntity = null, string mappedBy = null) : base(refEntity, mappedBy) {
        }
    }

}
