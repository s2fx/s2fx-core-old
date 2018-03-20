using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using S2fx.Model.Annotations;
using S2fx.Utility;

namespace S2fx.Model.Metadata {

    public abstract class AbstractMetadata {
        public IEnumerable<Attribute> Attributes { get; set; }

        public AbstractPropertyAttribute GetPropertyAttribute(Type attributeType) =>
            this.Attributes != null ?
                Attributes.Single(a => a.GetType() == attributeType) as AbstractPropertyAttribute : null;

        public T GetPropertyAttribute<T>() where T : AbstractPropertyAttribute =>
            GetPropertyAttribute(typeof(T)) as T;

    }

}
