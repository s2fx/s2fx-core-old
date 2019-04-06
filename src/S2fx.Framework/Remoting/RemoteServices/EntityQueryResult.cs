using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace S2fx.Remoting.RemoteServices {

    [DataContract]
    public class EntityQueryResult {

        [DataMember]
        public string Filter { get; set; }

        [DataMember]
        public string Select { get; set; }

        [DataMember]
        public string SortBy { get; set; }

        [DataMember]
        public long Offset { get; set; }

        [DataMember]
        public long Limit { get; set; }

        [DataMember]
        public long Count { get; set; }

        [DataMember]
        public long Total { get; set; }

        [DataMember]
        public IEnumerable<object> Entities { get; set; }
    }

}
