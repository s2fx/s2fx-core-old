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
        public MetaEntity Entity { get; set; }
        public IPropertyType Type { get; set; }
        public PropertyInfo ClrPropertyInfo { get; set; }
        public int Length { get; set; } = -1;

        public override void AcceptVisitor(IModelMetadataVisitor visitor) {
            visitor.VisitProperty(this);
        }
    }

    public class IdMetaProperty : MetaProperty {
    }

    public class RelationMetaProperty : MetaProperty {
        public string RefEntityName { get; set; }
        public string MappedByPropertyName { get; set; }
        public MetaEntity RefEntity { get; set; }
        public MetaProperty MappedBy { get; set; }
    }

    public class PrimitiveMetaProperty : RelationMetaProperty {
        public bool IsRequired { get; set; } = false;
    }

    public class ManyToOneMetaProperty : RelationMetaProperty {
        public bool IsRequired { get; set; } = false;
    }

    public class OneToManyMetaProperty : RelationMetaProperty {
    }

    public class ManyToManyMetaProperty : RelationMetaProperty {
        public string JoinTable { get; set; }
    }


}
