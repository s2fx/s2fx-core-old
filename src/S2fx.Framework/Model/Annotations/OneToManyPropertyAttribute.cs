using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class OneToManyPropertyAttribute : AbstractRelationPropertyAttribute {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.OneToManyTypeName;
        public bool IsOrphanRemoval { get; set; } = true;
        public string MappedBy { get; }
        public string RefEntity { get; }

        public OneToManyPropertyAttribute(string refEntity, string mappedBy) {
            this.RefEntity = refEntity;
            this.MappedBy = mappedBy;
        }
    }
}
