using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PrimitiveFieldAttribute : AbstractFieldAttribute {
        public override string FieldTypeName => "Primitive";
        public int Length { get; set; } = -1;
    }
}
