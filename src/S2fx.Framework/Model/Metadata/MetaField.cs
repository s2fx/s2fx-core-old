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
    public class MetaField : AnyMetadata {

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public bool IsReadOnly { get; set; } = false;

        [DataMember]
        public bool IsUnique { get; set; }

        [DataMember]
        public bool IsLazy { get; set; } = false;

        [DataMember]
        public bool IsSelect { get; set; }

        public MetaEntity Entity { get; set; }

        public IFieldType Type { get; set; }

        [DataMember(Name = "Type")]
        public string TypeName => this.Type.Name;

        public PropertyInfo ClrPropertyInfo { get; set; }

        [DataMember]
        public int? MaxLength { get; set; }

        public string DbName { get; set; } = null;

        public IList<AbstractMetaFieldAnnotation> Annotations { get; set; } = new List<AbstractMetaFieldAnnotation>();

        public override void AcceptVisitor(IMetadataVisitor visitor) {
            visitor.VisitField(this);
        }

        public AbstractMetaFieldAnnotation FindAnnotation(string name) => this.Annotations.FirstOrDefault(x => x.Name == name);
    }


    [DataContract]
    public class IdMetaField : MetaField {
    }


    [DataContract]
    public class RelationMetaField : MetaField {

        [DataMember(Name = "RefEntity")]
        public string RefEntityName { get; set; }

        [DataMember(Name = "MappedBy")]
        public string MappedByFieldName { get; set; }

        public MetaEntity RefEntity { get; set; }

        public MetaField MappedBy { get; set; }
    }


    [DataContract]
    public class PrimitiveMetaField : MetaField, IMetaFieldWithIsRequired {

        [DataMember]
        public bool IsRequired { get; set; }
    }


    [DataContract]
    public class ManyToOneMetaField : RelationMetaField, IMetaFieldWithIsRequired {

        [DataMember]
        public bool IsRequired { get; set; }
    }


    [DataContract]
    public class OneToManyMetaField : RelationMetaField {
    }


    [DataContract]
    public class ManyToManyMetaField : RelationMetaField {

        public string JoinTable { get; set; }
    }


    [DataContract]
    public class EnumerableMetaField : MetaField, IMetaFieldWithIsRequired {

        [DataMember]
        public bool IsRequired { get; set; } = true;
    }

}
