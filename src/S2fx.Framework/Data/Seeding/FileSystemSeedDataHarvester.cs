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
using S2fx.Data.Importing.Schemas;
using S2fx.Data.Sedding.Model;
using S2fx.Environment.Shell;
using S2fx.Xaml;

namespace S2fx.Data.Seeding {

    public class FileSystemSeedHarvester : ISeedHarvester {
        public const string SeedDataFolderName = "SeedData";
        public const string InitDataFolderName = "Init";
        public const string DemoDataFolderName = "Demo";
        public const string SeedConfigFileName = "SeedingManifest.xaml";

        readonly IHostingEnvironment _environment;
        readonly IXamlService _xaml;

        public FileSystemSeedHarvester(IHostingEnvironment environment, IXamlService xaml) {
            _environment = environment;
            _xaml = xaml;
        }

        public Task<IEnumerable<ImportingTaskDescriptor>> HarvestInitDataAsync(IFeatureInfo feature) =>
            this.HarvestSeedAsync(feature, InitDataFolderName);

        public Task<IEnumerable<ImportingTaskDescriptor>> HarvestDemoDataAsync(IFeatureInfo feature) =>
            this.HarvestSeedAsync(feature, DemoDataFolderName);

        private async Task<IEnumerable<ImportingTaskDescriptor>> HarvestSeedAsync(IFeatureInfo feature, string dataFolderName) {
            var initDataFolderSubPath = Path.Combine(feature.Extension.SubPath, SeedDataFolderName, dataFolderName);
            return await this.HarvestImportJobAsync(initDataFolderSubPath, feature);
        }

        private async Task<IEnumerable<ImportingTaskDescriptor>> HarvestImportJobAsync(string subPath, IFeatureInfo feature) {
            var manifestFile = _environment.ContentRootFileProvider
                .GetDirectoryContents(subPath)
                .Where(x => !x.IsDirectory && x.Name.Equals(SeedConfigFileName, StringComparison.InvariantCultureIgnoreCase))
                .SingleOrDefault();

            if (manifestFile == null) {
                return new ImportingTaskDescriptor[] { };
            }

            using (var stream = manifestFile.CreateReadStream()) {
                var sdd = await _xaml.LoadAsync<SeedDataDefinitions>(stream);
                var jobs = new List<ImportingTaskDescriptor>();
                foreach (var ds in sdd.DataSources) {
                    foreach (var importEntity in ds.Mappings) {
                        var job = new ImportingTaskDescriptor(
                            true, string.IsNullOrEmpty(importEntity.Feature) ? feature.Id : importEntity.Feature, subPath, ds, importEntity);
                        jobs.Add(job);
                    }
                }

                return jobs;
            }

        }


    }
}
