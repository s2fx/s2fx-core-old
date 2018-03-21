using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace S2fx.Environment.Extensions {

    public interface IS2ModuleManager {
        Task InstallAsync(string moduleName, bool withDemoData = false);
        Task UninstallAsync(string moduleName);
    }

    public class S2ModuleManager : IS2ModuleManager {
        public ILogger<S2ModuleManager> Logger { get; }

        public S2ModuleManager(ILogger<S2ModuleManager> logger) {
            this.Logger = logger;
        }

        public async Task InstallAsync(string moduleName, bool withDemoData = false) {
            this.Logger.LogInformation("Installing module: {0}", moduleName);
            throw new NotImplementedException();
        }

        public async Task UninstallAsync(string moduleName) {
            this.Logger.LogInformation("Uninstalling module: {0}", moduleName);
            throw new NotImplementedException();
        }
    }
}
