using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class AbstractPropertyAttribute : Attribute {
        public abstract string PropertyTypeName { get; }
        public FetchMethod Fetch { get; set; } = FetchMethod.Eager;
    }

}
