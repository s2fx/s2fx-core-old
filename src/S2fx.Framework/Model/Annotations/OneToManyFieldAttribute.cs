using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class OneToManyFieldAttribute : AbstractRelationalFieldAttribute {
        public override string FieldTypeName => BuiltinFieldTypeNames.OneToManyTypeName;
        public bool IsOrphanRemoval { get; set; } = true;

        public OneToManyFieldAttribute(string mappedBy, string refEntity = null) : base(refEntity, mappedBy) {
        }
    }
}
