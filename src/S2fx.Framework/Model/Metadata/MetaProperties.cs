using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Metadata {

    public class MetaProperty : AnyMetadata {

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsReadOnly { get; set; } = false;
        public MetaEntity Entity { get; set; }
        public IPropertyType Type { get; set; }
        public PropertyInfo ClrPropertyInfo { get; set; }
        public int MaxLength { get; set; } = -1;
        public string DbName { get; set; } = null;
        public IList<AbstractMetaPropertyAnnotation> Annotations { get; set; } = new List<AbstractMetaPropertyAnnotation>();

        public override void AcceptVisitor(IMetadataVisitor visitor) {
            visitor.VisitProperty(this);
        }

        public AbstractMetaPropertyAnnotation FindAnnotation(string name) => this.Annotations.FirstOrDefault(x => x.Name == name);
    }

    public class IdMetaProperty : MetaProperty {
    }

    public class RelationMetaProperty : MetaProperty {
        public string RefEntityName { get; set; }
        public string MappedByPropertyName { get; set; }
        public MetaEntity RefEntity { get; set; }
        public MetaProperty MappedBy { get; set; }
    }

    public class PrimitiveMetaProperty : RelationMetaProperty, IMetaPropertyWithIsRequired {
        public bool IsRequired { get; set; } = false;
    }

    public class ManyToOneMetaProperty : RelationMetaProperty, IMetaPropertyWithIsRequired {
        public bool IsRequired { get; set; } = false;
    }

    public class OneToManyMetaProperty : RelationMetaProperty {
    }

    public class ManyToManyMetaProperty : RelationMetaProperty {
        public string JoinTable { get; set; }
    }

    public class EnumerableMetaProperty : MetaProperty, IMetaPropertyWithIsRequired {
        public bool IsRequired { get; set; } = false;
    }
}
