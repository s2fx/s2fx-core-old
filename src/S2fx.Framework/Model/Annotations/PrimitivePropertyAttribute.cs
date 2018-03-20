using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PrimitivePropertyAttribute : AbstractPropertyAttribute {
        public override string PropertyTypeName => "Primitive";
        public int Length { get; set; } = -1;
    }
}
