using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace S2fx.Data.Importing.Model {


    public class PropertyBindingInfo {

        [XmlAttribute("source")]
        public string SourceExpression { get; set; }

        [XmlAttribute("property")]
        public string TargetProperty { get; set; }
    }

}
