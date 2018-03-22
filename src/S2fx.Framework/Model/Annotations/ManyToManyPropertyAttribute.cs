using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ManyToManyPropertyAttribute : AbstractRelationPropertyAttribute {
        public override string PropertyTypeName => "ManyToMany";

        public string JoinTable { get; }

        public ManyToManyPropertyAttribute(string mappedBy, string joinTable, string refEntity = null) : base(refEntity, mappedBy) {
            this.JoinTable = joinTable;
        }

    }
}
