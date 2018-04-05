using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Metadata {

    [DataContract]
    public class MetaProperty : AnyMetadata {

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public bool IsReadOnly { get; set; } = false;

        public MetaEntity Entity { get; set; }

        public IPropertyType Type { get; set; }

        [DataMember(Name = "Type")]
        public string PropertyTypeName => this.Type.Name;

        public PropertyInfo ClrPropertyInfo { get; set; }

        [DataMember]
        public int? MaxLength { get; set; }

        public string DbName { get; set; } = null;

        public IList<AbstractMetaPropertyAnnotation> Annotations { get; set; } = new List<AbstractMetaPropertyAnnotation>();

        public override void AcceptVisitor(IMetadataVisitor visitor) {
            visitor.VisitProperty(this);
        }

        public AbstractMetaPropertyAnnotation FindAnnotation(string name) => this.Annotations.FirstOrDefault(x => x.Name == name);
    }


    [DataContract]
    public class IdMetaProperty : MetaProperty {
    }


    [DataContract]
    public class RelationMetaProperty : MetaProperty {

        [DataMember(Name = "RefEntity")]
        public string RefEntityName { get; set; }

        [DataMember(Name = "MappedBy")]
        public string MappedByPropertyName { get; set; }

        public MetaEntity RefEntity { get; set; }

        public MetaProperty MappedBy { get; set; }
    }


    [DataContract]
    public class PrimitiveMetaProperty : MetaProperty, IMetaPropertyWithIsRequired {

        [DataMember]
        public bool IsRequired { get; set; }
    }


    [DataContract]
    public class ManyToOneMetaProperty : RelationMetaProperty, IMetaPropertyWithIsRequired {

        [DataMember]
        public bool IsRequired { get; set; }
    }


    [DataContract]
    public class OneToManyMetaProperty : RelationMetaProperty {
    }


    [DataContract]
    public class ManyToManyMetaProperty : RelationMetaProperty {

        public string JoinTable { get; set; }
    }


    [DataContract]
    public class EnumerableMetaProperty : MetaProperty, IMetaPropertyWithIsRequired {

        [DataMember]
        public bool IsRequired { get; set; } = true;
    }
}
