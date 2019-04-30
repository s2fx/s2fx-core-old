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
using S2fx.Environment.Shell;
using S2fx.Modules.Services;
using S2fx.Xaml;

namespace S2fx.Data.Importing.Seeds {

    public class S2StartupSeedHarvester : ISeedHarvester {

        readonly IHostingEnvironment _environment;
        readonly IXamlService _xaml;
        readonly IS2StartupService _s2StartupService;

        public S2StartupSeedHarvester(IHostingEnvironment environment, IXamlService xaml, IS2StartupService s2StartupService) {
            _environment = environment;
            _xaml = xaml;
            _s2StartupService = s2StartupService;
        }

        public Task<IEnumerable<ImportingJobDescriptor>> HarvestInitDataAsync(IFeatureInfo feature) =>
            this.HarvestImportJobAsync(feature, false);

        public Task<IEnumerable<ImportingJobDescriptor>> HarvestDemoDataAsync(IFeatureInfo feature) =>
            this.HarvestImportJobAsync(feature, true);

        private async Task<IEnumerable<ImportingJobDescriptor>> HarvestImportJobAsync(IFeatureInfo feature, bool isDemo) {
            var initManifests = new SeedManifestCollection();
            var demoManifests = new SeedManifestCollection();
            var startup = await _s2StartupService.GetOrDefaultByFeatureAsync(feature);
            if (startup == null) {
                return new ImportingJobDescriptor[] { };
            }
            startup.ConfigureSeeds(initManifests, demoManifests);
            var descriptors = isDemo ? demoManifests : initManifests;
            var allJobs = new List<ImportingJobDescriptor>();
            foreach (var descriptor in descriptors) {
                var jobs = await this.HarvestManifestAsync(feature, descriptor.Path);
                allJobs.AddRange(jobs);
            }
            return allJobs;
        }

        private async Task<IEnumerable<ImportingJobDescriptor>> HarvestManifestAsync(IFeatureInfo feature, string path) {
            var manifestPath = Path.Combine("Areas", feature.Id, path);
            var manifestFile = _environment.ContentRootFileProvider.GetFileInfo(manifestPath);
            if (manifestFile == null || manifestFile is NotFoundFileInfo) {
                return new ImportingJobDescriptor[] { };
            }
            using (var stream = manifestFile.CreateReadStream()) {
                var manifest = await _xaml.LoadAsync<SeedManifest>(stream);
                var dataSources = manifest.DataSources;
                var jobs = new List<ImportingJobDescriptor>();
                foreach (var ds in dataSources) {
                    foreach (var importEntity in ds.Mappings) {
                        var job = new ImportingJobDescriptor(
                            true, feature, ds, importEntity);
                        jobs.Add(job);
                    }
                }
                return jobs;
            }
        }


    }
}
