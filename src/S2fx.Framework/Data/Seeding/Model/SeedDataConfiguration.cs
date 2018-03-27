using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using S2fx.Data.Importing.Model;

namespace S2fx.Data.Sedding.Model {

    [XmlRoot("seed-data")]
    public class SeedDataConfiguration {

        [XmlElement("import")]
        public ImportDescriptor[] Jobs { get; set; } = new ImportDescriptor[] { };
    }

}
