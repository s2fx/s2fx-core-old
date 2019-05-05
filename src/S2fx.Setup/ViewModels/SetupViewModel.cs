using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Environment.Shell;

namespace S2fx.Setup.ViewModels {

    public class SetupViewModel {
        public string TenantName { get; set; }
        public string RootUserName { get; set; }
        public string RootPassword { get; set; }
        public string DatabaseConnectionString { get; set; }
        public bool IsDemo { get; set; }
        public IEnumerable<string> EnabledFeatures { get; set; }
    }

}
