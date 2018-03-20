using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Metadata {

    public class MetaProperty : AbstractMetadata {

        public string Name { get; set; }

        public IPropertyType Type { get; set; }
        public PropertyInfo ClrPropertyInfo { get; set; }
        public int Length { get; set; } = -1;
    }

    public class IdMetaProperty : MetaProperty {
    }

    public class PrimitiveMetaProperty : MetaProperty {
        public bool IsRequired { get; set; } = false;
    }

    public class ManyToOneMetaProperty : MetaProperty {
        public string RefEntity { get; set; }
        public string MappedBy { get; set; }
        public bool IsRequired { get; set; } = false;
    }

    public class OneToManyMetaProperty : MetaProperty {
        public string RefEntity { get; set; }
        public string MappedBy { get; set; }
    }


}
