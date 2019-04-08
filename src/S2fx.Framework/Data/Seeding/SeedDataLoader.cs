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
using S2fx.Services;

namespace S2fx.Data.Seeding {

    public class SeedDataLoader : ISeedDataLoader {

        readonly IHostingEnvironment _environment;
        readonly IDataImporter _importer;
        readonly ISeedDataHarvester _harvester;
        readonly IClock _clock;

        public ILogger Logger { get; }

        public SeedDataLoader(IHostingEnvironment environment,
            IDataImporter importer,
            ISeedDataHarvester harvester,
            IClock clock,
            ILogger<SeedDataLoader> logger) {
            _environment = environment;
            _importer = importer;
            _harvester = harvester;
            _clock = clock;
            this.Logger = logger;
        }

        public async Task LoadAllSeedDataAsync(bool withDemoData = false) {
            var allInitData = await _harvester.HarvestInitDataAsync();
            var allDemoData = await _harvester.HarvestDemoDataAsync();

            this.Logger.LogInformation("Loading all seed data for initialization...");

            var startedOn = _clock.Now();
            await this.LoadSeedDataAsync(allInitData.Select(x => x.Feature), false);

            if (withDemoData) {
                this.Logger.LogInformation("Loading all seed data for demostration...");
                await this.LoadSeedDataAsync(allDemoData.Select(x => x.Feature), true);
            }

            var elapsedTime = _clock.Now() - startedOn;
            this.Logger.LogInformation("All seed data loaded. Elapsed time: {0}", elapsedTime.ToString());
        }

        public async Task LoadSeedDataAsync(string feature, bool withDemoData = false) {
            await this.LoadSeedDataAsync(new string[] { feature }, false);

            if (withDemoData) {
                await this.LoadSeedDataAsync(new string[] { feature }, true);
            }
        }

        private async Task LoadSeedDataAsync(IEnumerable<string> features, bool isDemoData) {
            var jobs = !isDemoData ? await _harvester.HarvestInitDataAsync() : await _harvester.HarvestDemoDataAsync();
            jobs = jobs.Where(x => features.Contains(x.Feature));

            //TODO Sort

            foreach (var job in jobs) {
                this.Logger.LogInformation("Loading seed data file: [File={0}, Selector={1}]", job.File, job.EntityMapping.Selector);
                await _importer.ImportAsync(jobs);
            }

        }

    }
}
