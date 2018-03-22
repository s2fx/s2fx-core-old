using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Environment.Configuration {

    public class S2Settings {
        public string DefaultTimezone { get; set; } = "UTC";
        public DbSettings Db { get; set; }
    }

}
