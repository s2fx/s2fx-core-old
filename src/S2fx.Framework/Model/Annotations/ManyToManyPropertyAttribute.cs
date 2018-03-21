using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ManyToManyPropertyAttribute : AbstractRelationPropertyAttribute {
        public override string PropertyTypeName => "ManyToMany";

        public string JoinTable { get; }

        public ManyToManyPropertyAttribute(string refEntity = null, string mappedBy = null, string joinTable = null) : base(refEntity, mappedBy) {
            this.JoinTable = joinTable;
        }

    }
}
