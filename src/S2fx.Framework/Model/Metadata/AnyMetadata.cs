using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using S2fx.Model.Annotations;
using S2fx.Utility;

namespace S2fx.Model.Metadata {

    public abstract class AnyMetadata : IMetadataNode {

        public IEnumerable<Attribute> Attributes { get; set; }

        public AbstractFieldAttribute GetFieldAttribute(Type attributeType) =>
            this.Attributes != null ?
                Attributes.Single(a => a.GetType() == attributeType) as AbstractFieldAttribute : null;

        public T GetFieldAttribute<T>() where T : AbstractFieldAttribute =>
            GetFieldAttribute(typeof(T)) as T;

        public abstract void AcceptVisitor(IMetadataVisitor visitor);

    }

}
