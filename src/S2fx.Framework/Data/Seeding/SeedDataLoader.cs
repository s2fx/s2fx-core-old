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

namespace S2fx.Data.Seeding {

    public class SeedDataLoader : ISeedDataLoader {

        private readonly IHostingEnvironment _environment;
        private readonly IDataImporter _importer;
        private readonly ISeedDataHarvester _harvester;

        public ILogger Logger { get; }

        public SeedDataLoader(IHostingEnvironment environment,
            IDataImporter importer,
            ISeedDataHarvester harvester,
            ILogger<SeedDataLoader> logger) {
            _environment = environment;
            _importer = importer;
            _harvester = harvester;
            this.Logger = logger;
        }

        public async Task LoadAllSeedDataAsync(bool withDemoData = false) {
            var allInitData = await _harvester.HarvestInitDataAsync();
            var allDemoData = await _harvester.HarvestDemoDataAsync();

            await this.LoadSeedDataAsync(allInitData.Select(x => x.Feature), false);

            if (withDemoData) {
                await this.LoadSeedDataAsync(allDemoData.Select(x => x.Feature), true);
            }
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
                if (this.Logger.IsEnabled(LogLevel.Information)) {
                    this.Logger.LogInformation("Loading seed data: [File={0}, Selector={1}]", job.File, job.Selector);
                    await _importer.ImportAsync(jobs);
                }
            }

        }

    }
}
