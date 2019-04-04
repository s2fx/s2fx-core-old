using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Annotations {

    public abstract class AbstractRelationalFieldAttribute : AbstractFieldAttribute {
        public CascadeMethod Cascade { get; set; } = CascadeMethod.All;
        public string RefEntity { get; }
        public string MappedBy { get; }

        public AbstractRelationalFieldAttribute(string refEntity, string mappedBy) {
            this.RefEntity = refEntity;
            this.MappedBy = mappedBy;
        }
    }
}
