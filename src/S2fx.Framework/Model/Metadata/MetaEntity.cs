using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Environment.Extensions.Features;

namespace S2fx.Model.Metadata {

    public class MetaEntity : AnyMetadata {

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public Type ClrType { get; set; }

        public IDictionary<string, MetaProperty> Properties { get; } = new Dictionary<string, MetaProperty>();

        public string DbName { get; set; }

        public override void AcceptVisitor(IMetadataVisitor visitor) {
            visitor.VisitEntity(this);
        }

        public override string ToString() => $"[{this.Name}]{this.DisplayName}";
    }

}
