using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.Environment.Extensions;
using OrchardCore.Environment.Extensions.Features;
using OrchardCore.Environment.Shell.Descriptor.Models;

namespace S2fx.Environment.Shell {

    public class ShellFeatureService : IShellFeatureService {
        private readonly ShellDescriptor _shell;
        private readonly IExtensionManager _extensions;

        public ShellFeatureService(ShellDescriptor shell, IExtensionManager extensions) {
            _shell = shell;
            _extensions = extensions;
        }

        public async Task<IEnumerable<FeatureEntry>> GetEnabledFeatureEntriesAsync() {
            var enabledFeatureIds = new HashSet<string>(_shell.Features.Select(x => x.Id));
            var allFeatures = await _extensions.LoadFeaturesAsync();
            return allFeatures.Where(x => enabledFeatureIds.Contains(x.FeatureInfo.Id));
        }

    }
}
