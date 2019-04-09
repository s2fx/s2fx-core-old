using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Hosting;
using OrchardCore.Environment.Extensions;
using OrchardCore.Environment.Extensions.Features;
using S2fx.Data.Importing.Model;
using S2fx.Data.Sedding.Model;
using S2fx.Environment.Shell;

namespace S2fx.Data.Seeding {

    public class FileSystemSeedHarvester : ISeedHarvester {
        public const string SeedDataFolderName = "Seed";
        public const string InitDataFolderName = "Init";
        public const string DemoDataFolderName = "Demo";
        public const string SeedConfigFileName = "Seeding.config.xml";

        private IHostingEnvironment _environment;
        private IShellFeatureEntityService _shellFeatureService;

        public FileSystemSeedHarvester(IHostingEnvironment environment, IShellFeatureEntityService shellFeatureService) {
            _environment = environment;
            _shellFeatureService = shellFeatureService;
        }

        public Task<IEnumerable<ImportDescriptor>> HarvestInitDataAsync() =>
            this.HarvestSeedAsync(InitDataFolderName);

        public Task<IEnumerable<ImportDescriptor>> HarvestDemoDataAsync() =>
            this.HarvestSeedAsync(DemoDataFolderName);

        private async Task<IEnumerable<ImportDescriptor>> HarvestSeedAsync(string dataFolderName) {
            var features = await _shellFeatureService.GetEnabledFeatureEntriesAsync();
            features = features.OrderBy(x => x.FeatureInfo.Priority);
            var allJobs = new List<ImportDescriptor>();

            foreach (var feature in features) {
                var initDataFolderSubPath = Path.Combine(feature.FeatureInfo.Extension.SubPath, SeedDataFolderName, dataFolderName);
                var jobs = await this.HarvestImportJobAsync(initDataFolderSubPath, feature.FeatureInfo);
                allJobs.AddRange(jobs);
            }
            return allJobs;
        }

        private Task<IEnumerable<ImportDescriptor>> HarvestImportJobAsync(string subPath, IFeatureInfo feature) {
            var initDataConfigFile = _environment.ContentRootFileProvider
                .GetDirectoryContents(subPath)
                .Where(x => !x.IsDirectory && x.Name.Equals(SeedConfigFileName, StringComparison.InvariantCultureIgnoreCase))
                .SingleOrDefault();

            if (initDataConfigFile == null) {
                return Task.FromResult((new ImportDescriptor[] { }).AsEnumerable());
            }

            using (var stream = initDataConfigFile.CreateReadStream()) {
                var xs = new XmlSerializer(typeof(SeedDataConfiguration));
                var cfg = (SeedDataConfiguration)xs.Deserialize(stream);
                var jobs = cfg.Jobs.Where(x => (x.Feature != null && x.Feature == feature.Id) || (feature.Extension.Id == feature.Id && x.Feature == null));
                foreach (var job in jobs) {
                    job.FileProvider = _environment.ContentRootFileProvider;
                    var jobDataFile = _environment.ContentRootFileProvider.GetFileInfo(Path.Combine(subPath, job.File));
                    if (!jobDataFile.Exists) {
                        throw new FileNotFoundException($"Failed to load seed data file: {job.File}", job.File);
                    }
                    job.ImportFileInfo = jobDataFile;
                    job.Feature = job.Feature ?? feature.Id;
                }

                return Task.FromResult(jobs);
            }

        }


    }
}
