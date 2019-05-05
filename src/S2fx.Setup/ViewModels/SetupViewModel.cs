using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Environment.Shell;

namespace S2fx.Setup.ViewModels {

    public class SetupViewModel {
        public string TenantName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string DatabaseConnectionString { get; set; }
        public bool IsDemo { get; set; }
        public IEnumerable<string> EnabledFeatures { get; set; }
    }

}
