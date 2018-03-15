using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Environment.Extensions {

    public interface IS2ModuleManager {
        Task InstallAsync(Module module, bool withDemoData = false);
        Task UninstallAsync(Module module);
    }

    public class S2ModuleManager : IS2ModuleManager {

        public Task InstallAsync(Module module, bool withDemoData = false) {
            throw new NotImplementedException();
        }

        public Task UninstallAsync(Module module) {
            throw new NotImplementedException();
        }
    }
}
