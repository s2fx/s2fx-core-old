using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace S2fx.Remoting.RemoteServices {

    [DataContract]
    public class EntityQueryResult {

        [DataMember]
        public long Count { get; set; }

        [DataMember]
        public IEnumerable<object> Value { get; set; }
    }

}
