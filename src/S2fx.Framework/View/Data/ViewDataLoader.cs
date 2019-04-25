using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using OrchardCore.Modules;
using S2fx.Data.Importing;
using S2fx.Data.Seeding;

namespace S2fx.View.Data {

    public class ViewDataLoader : IViewDataLoader {

        readonly IHostingEnvironment _environment;
        readonly IDataImporter _importer;
        readonly ISeedHarvester _harvester;
        readonly IClock _clock;

        public ILogger Logger { get; }

        public ViewDataLoader(IHostingEnvironment environment,
            IDataImporter importer,
            ISeedHarvester harvester,
            IClock clock,
            ILogger<ViewDataLoader> logger) {
            _environment = environment;
            _importer = importer;
            _harvester = harvester;
            _clock = clock;
            this.Logger = logger;
        }

        public async Task LoadAllViewsAsync() {
            this.Logger.LogInformation("Loading all seed data for initialization...");

            var startedOn = _clock.UtcNow;

            var allInitData = await _harvester.HarvestInitDataAsync();

            await this.LoadViewAsync(allInitData.Select(x => x.Feature));

            var elapsedTime = _clock.UtcNow - startedOn;
            this.Logger.LogInformation("All seed data loaded. Elapsed time: {0}", elapsedTime.ToString());
        }

        public async Task LoadViewAsync(string feature) {
            await this.LoadViewAsync(new string[] { feature });
        }

        private async Task LoadViewAsync(IEnumerable<string> features) {
            var jobs = await _harvester.HarvestInitDataAsync();
            jobs = jobs.Where(x => features.Contains(x.Feature));
            //TODO Sort

            foreach (var job in jobs) {
                this.Logger.LogInformation("Loading seed data file: [File={0}, Selector={1}]", job.File, job.EntityMapping.Selector);
                await _importer.ImportAsync(jobs);
            }

        }

    }

}
