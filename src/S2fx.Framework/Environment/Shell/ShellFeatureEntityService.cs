using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.Environment.Extensions;
using OrchardCore.Environment.Extensions.Features;
using OrchardCore.Environment.Shell.Descriptor;
using OrchardCore.Environment.Shell.Descriptor.Models;

namespace S2fx.Environment.Shell {

    public class ShellFeatureEntityService : IShellFeatureEntityService {
        private readonly IShellDescriptorManager _shellDescriptorManager;
        private readonly IExtensionManager _extensions;

        public ShellFeatureEntityService(IShellDescriptorManager shellDescriptorManager, IExtensionManager extensions) {
            _shellDescriptorManager = shellDescriptorManager;
            _extensions = extensions;
        }

        public async Task<IEnumerable<FeatureEntry>> GetEnabledFeatureEntriesAsync() {
            var shell = await _shellDescriptorManager.GetShellDescriptorAsync();
            var enabledFeatureIds = new HashSet<string>(shell.Features.Select(x => x.Id));
            var allFeatures = await _extensions.LoadFeaturesAsync();
            return allFeatures.Where(x => enabledFeatureIds.Contains(x.FeatureInfo.Id));
        }

    }
}
