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
        public FieldMapping[] FieldMappings { get; set; } = new FieldMapping[] { };
    }

    public class FieldMapping : MappingNode {

        [XmlAttribute("source")]
        public string SourceExpression { get; set; }

        [XmlAttribute("field")]
        public string TargetField { get; set; }

        [XmlAttribute("format")]
        public string Format { get; set; }

        [Required, XmlElement("map")]
        public FieldMapping[] Children { get; set; } = new FieldMapping[] { };
    }

}
