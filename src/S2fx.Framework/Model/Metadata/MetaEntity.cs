using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using OrchardCore.Environment.Extensions.Features;

namespace S2fx.Model.Metadata {

    [DataContract]
    public class MetaEntity : AnyMetadata {

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        public Type ClrType { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public IEnumerable<string> Dependencies { get; set; }

        public IFeatureInfo Feature { get; set; }

        [DataMember(Name = "feature")]
        public string FeatureId => this.Feature.Id;

        [DataMember]
        public IDictionary<string, MetaProperty> Properties { get; } = new Dictionary<string, MetaProperty>();

        public string DbName { get; set; }

        public IList<AbstractMetaEntityAnnotation> Annotations { get; set; } = new List<AbstractMetaEntityAnnotation>();

        public AbstractMetaEntityAnnotation FindAnnotation(string name) => this.Annotations.FirstOrDefault(x => x.Name == name);

        public override void AcceptVisitor(IMetadataVisitor visitor) {
            visitor.VisitEntity(this);
        }

        public override string ToString() => $"[{this.Name}]{this.DisplayName}";
    }

}
