using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Extensions.FileProviders;

namespace S2fx.Data.Importing.Model {

    public class ImportDescriptor {

        [Required, XmlAttribute("format")]
        public string Format { get; set; }

        [Required, XmlAttribute("entity")]
        public string Entity { get; set; }

        [XmlAttribute("feature")]
        public string Feature { get; set; }

        [XmlAttribute("update")]
        public bool CanUpdate { get; set; } = false;

        [Required, XmlAttribute("file")]
        public string File { get; set; }

        [Required, XmlAttribute("path")]
        public string Selector { get; set; }

        [XmlAttribute("where")]
        public string Where { get; set; }

        [XmlIgnore]
        public IFileInfo ImportFileInfo { get; set; }

        [XmlIgnore]
        public IFileProvider FileProvider { get; set; }

        [XmlElement("bind")]
        public PropertyBindingInfo[] PropertyBindingInfos { get; set; }
    }

}
