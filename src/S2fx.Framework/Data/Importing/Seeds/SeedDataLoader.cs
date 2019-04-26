using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell.Descriptor.Models;
using OrchardCore.Environment.Extensions;
using S2fx.Environment.Shell;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using S2fx.Data.Importing;
using Microsoft.Extensions.Logging;
using OrchardCore.Modules;
using OrchardCore.Environment.Shell;
using S2fx.Utility;
using OrchardCore.Environment.Extensions.Features;

namespace S2fx.Data.Importing.Seeds {

    public class SeedDataLoader : ISeedLoader {

        readonly IHostingEnvironment _environment;
        readonly IShellFeaturesManager _shellFeaturesManager;
        readonly IDataImporter _importer;
        readonly ISeedHarvester _harvester;
        readonly IClock _clock;

        public ILogger Logger { get; }

        public SeedDataLoader(IHostingEnvironment environment,
            IShellFeaturesManager shellFeaturesManager,
            IDataImporter importer,
            ISeedHarvester harvester,
            IClock clock,
            ILogger<SeedDataLoader> logger) {
            _environment = environment;
            _shellFeaturesManager = shellFeaturesManager;
            _importer = importer;
            _harvester = harvester;
            _clock = clock;
            this.Logger = logger;
        }

        public async Task LoadAllSeedsAsync(bool withDemoData = false) {
            this.Logger.LogInformation("Loading all seed data for initialization...");

            var startedOn = _clock.UtcNow;

            var sortedFeatures = (await _shellFeaturesManager.GetEnabledFeaturesAsync())
                .DependencySort(x => x.Id, x => x.Dependencies);

            foreach (var feature in sortedFeatures) {
                await this.DoLoadSeedAsync(feature, withDemoData);
            }

            var elapsedTime = _clock.UtcNow - startedOn;
            this.Logger.LogInformation("All seed data loaded. Elapsed time: {0}", elapsedTime.ToString());
        }

        public async Task LoadSeedAsync(string featureId, bool withDemoData = false) {
            if (string.IsNullOrEmpty(featureId)) {
                throw new ArgumentNullException(nameof(featureId));
            }
            var feature = (await _shellFeaturesManager.GetEnabledFeaturesAsync()).Single(x => x.Id == featureId);
            await this.DoLoadSeedAsync(feature, withDemoData);
        }

        private async Task DoLoadSeedAsync(IFeatureInfo feature, bool withDemoData = false) {
            this.Logger.LogInformation($"Loading seed data for feature: '{feature.Id}'");

            var initDataJobs = await _harvester.HarvestInitDataAsync(feature);
            await _importer.ImportAsync(initDataJobs);

            if (withDemoData) {
                var demoJobs = await _harvester.HarvestDemoDataAsync(feature);
                await _importer.ImportAsync(demoJobs);
            }
        }

    }
}
