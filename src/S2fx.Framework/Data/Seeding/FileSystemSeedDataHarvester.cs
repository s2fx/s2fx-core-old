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
using OrchardCore.Environment.Shell;
using S2fx.Data.Importing.Model;
using S2fx.Data.Sedding.Model;
using S2fx.Environment.Shell;

namespace S2fx.Data.Seeding {

    public class FileSystemSeedHarvester : ISeedHarvester {
        public const string SeedDataFolderName = "SeedData";
        public const string InitDataFolderName = "Init";
        public const string DemoDataFolderName = "Demo";
        public const string SeedConfigFileName = "Seeding.config.xml";

        private IHostingEnvironment _environment;

        public FileSystemSeedHarvester(IHostingEnvironment environment) {
            _environment = environment;
        }

        public Task<IEnumerable<ImportingTaskDescriptor>> HarvestInitDataAsync(IFeatureInfo feature) =>
            this.HarvestSeedAsync(feature, InitDataFolderName);

        public Task<IEnumerable<ImportingTaskDescriptor>> HarvestDemoDataAsync(IFeatureInfo feature) =>
            this.HarvestSeedAsync(feature, DemoDataFolderName);

        private async Task<IEnumerable<ImportingTaskDescriptor>> HarvestSeedAsync(IFeatureInfo feature, string dataFolderName) {
            var initDataFolderSubPath = Path.Combine(feature.Extension.SubPath, SeedDataFolderName, dataFolderName);
            return await this.HarvestImportJobAsync(initDataFolderSubPath, feature);
        }

        private Task<IEnumerable<ImportingTaskDescriptor>> HarvestImportJobAsync(string subPath, IFeatureInfo feature) {
            var initDataConfigFile = _environment.ContentRootFileProvider
                .GetDirectoryContents(subPath)
                .Where(x => !x.IsDirectory && x.Name.Equals(SeedConfigFileName, StringComparison.InvariantCultureIgnoreCase))
                .SingleOrDefault();

            if (initDataConfigFile == null) {
                return Task.FromResult((new ImportingTaskDescriptor[] { }).AsEnumerable());
            }

            using (var stream = initDataConfigFile.CreateReadStream()) {
                var xs = new XmlSerializer(typeof(SeedDataConfiguration));
                var cfg = (SeedDataConfiguration)xs.Deserialize(stream);
                var jobs = cfg.Jobs.Where(x => (x.Feature != null && x.Feature == feature.Id) || (feature.Extension.Id == feature.Id && x.Feature == null));
                foreach (var job in jobs) {
                    job.IsSudo = true; // 种子数据总是忽略权限检查
                    job.File = Path.Combine(subPath, job.File);
                    job.Feature = job.Feature ?? feature.Id;
                }

                return Task.FromResult(jobs);
            }

        }


    }
}
