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

namespace S2fx.Data.Seeding {

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

            var allInitData = await _harvester.HarvestInitDataAsync();

            await this.LoadSeedDataAsync(allInitData.Select(x => x.Feature), false);

            if (withDemoData) {
                var allDemoData = await _harvester.HarvestDemoDataAsync();
                this.Logger.LogInformation("Loading all seed data for demostration...");
                await this.LoadSeedDataAsync(allDemoData.Select(x => x.Feature), true);
            }

            var elapsedTime = _clock.UtcNow - startedOn;
            this.Logger.LogInformation("All seed data loaded. Elapsed time: {0}", elapsedTime.ToString());
        }

        public async Task LoadSeedAsync(string feature, bool withDemoData = false) {
            await this.LoadSeedDataAsync(new string[] { feature }, false);

            if (withDemoData) {
                await this.LoadSeedDataAsync(new string[] { feature }, true);
            }
        }

        private async Task LoadSeedDataAsync(IEnumerable<string> features, bool isDemoData) {
            var tasks = !isDemoData ? await _harvester.HarvestInitDataAsync() : await _harvester.HarvestDemoDataAsync();
            tasks = tasks.Where(x => features.Contains(x.Feature));

            var featureInfos = (await _shellFeaturesManager.GetEnabledFeaturesAsync()).ToDictionary(x => x.Id);

            var sortedTasks = tasks.Select(x => (task: x, featureInfo: featureInfos[x.Feature]))
                             .DependencySort(x => x.featureInfo.Id, x => x.featureInfo.Dependencies)
                             .Select(x => x.task);

            foreach (var job in tasks) {
                this.Logger.LogInformation("Loading seed data file: [File={0}, Selector={1}]", job.File, job.EntityMapping.Selector);
                await _importer.ImportAsync(tasks);
            }

        }

    }
}
