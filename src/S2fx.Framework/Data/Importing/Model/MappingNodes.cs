using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace S2fx.Data.Importing.Model {

    public abstract class MappingNode {
        [XmlAttribute("where")]
        public string Where { get; set; }

        [XmlAttribute("update")]
        public bool CanUpdate { get; set; } = false;

    }

    public class EntityMapping : MappingNode {

        [Required, XmlAttribute("selector")]
        public string Selector { get; set; }

        [Required, XmlElement("map")]
        public PropertyMapping[] PropertyMappings { get; set; } = new PropertyMapping[] { };
    }

    public class PropertyMapping : MappingNode {

        [XmlAttribute("source")]
        public string SourceExpression { get; set; }

        [XmlAttribute("property")]
        public string TargetProperty { get; set; }

        [XmlAttribute("format")]
        public string Format { get; set; }

        [Required, XmlElement("map")]
        public PropertyMapping[] Children { get; set; } = new PropertyMapping[] { };

        [XmlIgnore]
        public Func<object, string> SourceGetter { get; set; }
    }

}
