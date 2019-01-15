using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OrchardCore.Modules;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Descriptor.Models;
using OrchardCore.Environment.Shell.Descriptor;
using System.Linq;
using OrchardCore.Environment.Extensions;

namespace S2fx.Environment.Shell.Descriptor {

    /// <summary>
    /// Implements <see cref="IShellDescriptorManager"/> by returning a single tenant with all the available 
    /// extensions.
    /// </summary>
    public class S2ShellDescriptorManager : IShellDescriptorManager {
        private readonly IExtensionManager _extensionManager;
        private ShellDescriptor _shellDescriptor;

        public S2ShellDescriptorManager(IExtensionManager extensionManager) {
            _extensionManager = extensionManager;
        }

        public Task<ShellDescriptor> GetShellDescriptorAsync() {
            if (_shellDescriptor == null) {
                _shellDescriptor = new ShellDescriptor {
                    Features = _extensionManager.GetFeatures().Select(x => new ShellFeature { Id = x.Id }).ToList()
                };
            }

            return Task.FromResult(_shellDescriptor);
        }

        public Task UpdateShellDescriptorAsync(int priorSerialNumber, IEnumerable<ShellFeature> enabledFeatures, IEnumerable<ShellParameter> parameters) {
            return Task.CompletedTask;
        }
    }
}
