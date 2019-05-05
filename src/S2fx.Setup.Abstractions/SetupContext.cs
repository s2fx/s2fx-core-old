using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrchardCore.Environment.Shell;

namespace S2fx.Setup {

    public class SetupContext : ISetupInfo {
        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        public ShellSettings ShellSettings { get; set; }
        public string RootUserName { get; set; }
        public string RootPassword { get; set; }
        public string DbName { get; set; }
        public string DatabaseConnectionString { get; set; }
        public bool IsDemo { get; set; }
        public IEnumerable<string> EnabledFeatures { get; set; }
        public IReadOnlyDictionary<string, string> Errors => _errors;
        public bool IsFailed => _errors.Any();

        public void SetError(string key, string msg) {
            _errors[key] = msg;
        }
    }

}
