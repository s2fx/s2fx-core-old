using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using S2fx.Data.Importing.Model;

namespace S2fx.Data.Sedding.Model {

    public class SeedDataConfiguration {

        [XmlArrayItem("import")]
        public ImportJobInfo[] Jobs { get; set; }
    }

}
