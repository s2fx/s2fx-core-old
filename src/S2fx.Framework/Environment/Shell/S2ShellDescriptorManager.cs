using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.Environment.Shell.Descriptor;
using OrchardCore.Environment.Shell.Descriptor.Models;

namespace S2fx.Environment.Shell {

    public class S2ShellDescriptorManager : IShellDescriptorManager {

        public Task<ShellDescriptor> GetShellDescriptorAsync() {
            throw new NotImplementedException();
        }

        public Task UpdateShellDescriptorAsync(int priorSerialNumber, IEnumerable<ShellFeature> enabledFeatures, IEnumerable<ShellParameter> parameters) {
            throw new NotImplementedException();
        }

    }

}
