using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace S2fx.Setup.Model {

    [DataContract]
    public class DbProviderInfo {

        [DataMember]
        public string Name { get; }

        [DataMember]
        public string DisplayName { get; }

        [DataMember]
        public bool IsLoginNeeded { get; }

        public DbProviderInfo(string name, string displayName, bool isLoginNeeded) {
            this.Name = name;
            this.DisplayName = displayName;
            this.IsLoginNeeded = isLoginNeeded;
        }
    }

}
