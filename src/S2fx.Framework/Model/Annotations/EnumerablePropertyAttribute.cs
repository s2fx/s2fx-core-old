using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EnumerablePropertyAttribute : AbstractPropertyAttribute {
        public override string FieldTypeName => BuiltinFieldTypeNames.EnumerableTypeName;
        public int Length { get; set; } = -1;
    }
}
