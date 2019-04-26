using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using OrchardCore.Environment.Extensions;
using OrchardCore.Environment.Extensions.Features;
using OrchardCore.Environment.Shell;
using S2fx.Data.Importing.Model;
using S2fx.Data.Importing.Schemas;
using S2fx.Data.Sedding.Model;
using S2fx.Environment.Shell;
using S2fx.Xaml;

namespace S2fx.Data.Seeding {

    public class ModularSeedHarvester : ISeedHarvester {
        public const string SeedingManifestFileName = "SeedingManifest.xaml";

        readonly IHostingEnvironment _environment;
        readonly IXamlService _xaml;

        public ModularSeedHarvester(IHostingEnvironment environment, IXamlService xaml) {
            _environment = environment;
            _xaml = xaml;
        }

        public Task<IEnumerable<ImportingTaskDescriptor>> HarvestInitDataAsync(IFeatureInfo feature) =>
            this.HarvestImportJobAsync(feature, false);

        public Task<IEnumerable<ImportingTaskDescriptor>> HarvestDemoDataAsync(IFeatureInfo feature) =>
            this.HarvestImportJobAsync(feature, true);

        private async Task<IEnumerable<ImportingTaskDescriptor>> HarvestImportJobAsync(IFeatureInfo feature, bool isDemo) {
            var manifestPath = Path.Combine("Areas", feature.Id, SeedingManifestFileName);
            var manifestFile = _environment.ContentRootFileProvider.GetFileInfo(manifestPath);
            if (manifestFile == null || manifestFile is NotFoundFileInfo) {
                return new ImportingTaskDescriptor[] { };
            }

            using (var stream = manifestFile.CreateReadStream()) {
                var manifest = await _xaml.LoadAsync<SeedingManifest>(stream);
                var sdd = isDemo ? manifest.DemoData : manifest.InitData;
                var jobs = new List<ImportingTaskDescriptor>();
                foreach (var ds in sdd) {
                    foreach (var importEntity in ds.Mappings) {
                        var job = new ImportingTaskDescriptor(
                            true, string.IsNullOrEmpty(importEntity.Feature) ? feature.Id : importEntity.Feature, ds, importEntity);
                        jobs.Add(job);
                    }
                }

                return jobs;
            }

        }


    }
}
