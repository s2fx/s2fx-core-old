using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ManyToOnePropertyAttribute : AbstractRelationPropertyAttribute {
        public override string PropertyTypeName => "ManyToOne";

        public ManyToOnePropertyAttribute(string refEntity = null, string mappedBy = null) : base(refEntity, mappedBy) {
        }
    }

}
