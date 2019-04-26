using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Environment.Configuration {

    public class S2AppSettings {
        public string DefaultTimezone { get; set; } = "UTC";
        public string DbProvider { get; set; }
    }

}
