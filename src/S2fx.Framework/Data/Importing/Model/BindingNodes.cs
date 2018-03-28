using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace S2fx.Data.Importing.Model {

    public abstract class BindingNode {
        [XmlAttribute("where")]
        public string Where { get; set; }

        [XmlAttribute("update")]
        public bool CanUpdate { get; set; } = false;

    }

    public class EntityBinding : BindingNode {

        [Required, XmlAttribute("selector")]
        public string Selector { get; set; }

        [Required, XmlElement("property")]
        public PropertyBinding[] Properties { get; set; } = new PropertyBinding[] { };
    }

    public class PropertyBinding : BindingNode {

        [XmlAttribute("source")]
        public string SourceExpression { get; set; }

        [XmlAttribute("target")]
        public string TargetProperty { get; set; }

        [Required, XmlElement("property")]
        public PropertyBinding[] Children { get; set; } = new PropertyBinding[] { };
    }

}
