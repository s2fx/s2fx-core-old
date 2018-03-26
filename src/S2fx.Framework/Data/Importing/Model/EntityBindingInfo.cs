using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace S2fx.Data.Importing.Model {

    public class EntityBindingInfo {

        [Required, XmlAttribute("kind")]
        public string Kind { get; set; }

        [XmlArrayItem("bind-property")]
        public PropertyBindingInfo[] Properties { get; set; }
    }

}
