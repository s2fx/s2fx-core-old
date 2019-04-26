using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Environment.Configuration {

    public interface IConfigurationLoader {
        S2AppSettings GetSettings();
    }

}
