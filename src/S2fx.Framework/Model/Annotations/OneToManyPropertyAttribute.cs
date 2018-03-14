using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class OneToManyPropertyAttribute : AbstractRelationPropertyAttribute {
        public bool IsOrphanRemoval { get; set; } = true;
        public string MappedBy { get; }

        public OneToManyPropertyAttribute(string mappedBy) {
            this.MappedBy = mappedBy;
        }
    }
}
