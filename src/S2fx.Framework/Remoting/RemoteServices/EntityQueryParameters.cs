using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace S2fx.Remoting.RemoteServices {

    [DataContract]
    public class EntityQueryParameters {
        [DataMember]
        public string QueryString { get; set; }
    }

}
