using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace S2fx.Data.Importing.Model {

    public class ImportJobInfo {

        [Required, XmlAttribute("entity")]
        public string Entity { get; set; }

        [XmlAttribute("feature")]
        public string Feature { get; set; }

        [XmlAttribute("update")]
        public bool CanUpdate { get; set; } = false;

        [XmlAttribute("file")]
        public string File { get; set; }

        [XmlAttribute("path")]
        public string Path { get; set; }
    }

}
