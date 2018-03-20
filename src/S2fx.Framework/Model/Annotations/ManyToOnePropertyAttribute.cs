using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ManyToOnePropertyAttribute : AbstractRelationPropertyAttribute {
        public override string PropertyTypeName => "ManyToOne";
        public string RefEntity { get; }
        public string MappedBy { get; }

        public ManyToOnePropertyAttribute(string refEntity, string mappedBy = null) {
            this.RefEntity = refEntity;
            this.MappedBy = mappedBy;
        }
    }

}
