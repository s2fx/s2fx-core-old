using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Setup.Model {

    public class SetupContext {
        public string AdminPassword { get; set; }
        public string DbName { get; set; }
        public string IsDemo { get; set; }
        public IEnumerable<string> EnabledFeatures { get; set; }
    }

}
