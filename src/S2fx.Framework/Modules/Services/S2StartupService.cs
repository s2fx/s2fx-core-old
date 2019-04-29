using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OrchardCore.Environment.Extensions.Features;
using S2fx.Environment.Shell;

namespace S2fx.Modules.Services {

    public class S2StartupService : IS2StartupService {

        readonly IShellFeatureEntityService _shellFeatureEntityService;
        readonly IEnumerable<OrchardCore.Modules.IStartup> _orchardStartups;

        public ILogger Logger { get; }

        public S2StartupService(
            IShellFeatureEntityService shellFeatureEntityService,
            IEnumerable<OrchardCore.Modules.IStartup> orchardStartups,
            ILogger<S2StartupService> logger) {
            _shellFeatureEntityService = shellFeatureEntityService;
            _orchardStartups = orchardStartups;
            this.Logger = logger;
        }

        public async Task<IS2Startup> GetOrDefaultByFeatureAsync(IFeatureInfo feature) {
            var s2StartupType = typeof(IS2Startup);
            var features = await _shellFeatureEntityService.GetEnabledFeatureEntriesAsync();
            var featureEntry = features.Single(x => x.FeatureInfo.Id == feature.Id);
            var startupType = featureEntry.ExportedTypes.SingleOrDefault(x => s2StartupType.IsAssignableFrom(x));
            return _orchardStartups.SingleOrDefault(x => x.GetType() == startupType) as IS2Startup;
        }
    }

}
