using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace S2fx.Setup.Model {

    [DataContract]
    public class SetupOptions {

        [DataMember]
        public string AdminPassword { get; set; }

        [DataMember]
        public string DbName { get; set; }

        [DataMember]
        public string IsDemo { get; set; }

        [DataMember]
        public IEnumerable<string> EnabledFeatures { get; set; }
    }

}
