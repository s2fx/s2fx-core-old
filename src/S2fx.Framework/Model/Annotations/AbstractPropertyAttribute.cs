using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class AbstractPropertyAttribute : Attribute {
        public FetchMethod Fetch { get; set; } = FetchMethod.Eager;
    }

}
