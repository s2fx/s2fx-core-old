using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Setup.Services {

    public class SetupContext {
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }
        public string DbProvider { get; set; }
        public string DbConnectionString { get; set; }
        public string IsDemo { get; set; }
        public IEnumerable<string> EnabledFeatures { get; set; }
    }

}
