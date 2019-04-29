using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using OrchardCore.Environment.Extensions.Features;
using S2fx.Data.Importing.Model;
using S2fx.Environment.Shell;
using S2fx.View.Schemas;
using S2fx.Xaml;
using S2fx.Modules;

namespace S2fx.View.Data {

    public class S2StartupViewHarvester : IViewHarvester {

        readonly IHostingEnvironment _environment;
        readonly IXamlService _xaml;
        readonly IShellFeatureEntityService _shellFeatureEntityService;
        readonly IEnumerable<OrchardCore.Modules.IStartup> _orchardStartups;

        public ILogger Logger { get; }

        public S2StartupViewHarvester(IHostingEnvironment environment,
            IXamlService xaml,
            IShellFeatureEntityService shellFeatureEntityService,
            IEnumerable<OrchardCore.Modules.IStartup> orchardStartups,
            ILogger<S2StartupViewHarvester> logger) {
            _environment = environment;
            _xaml = xaml;
            _shellFeatureEntityService = shellFeatureEntityService;
            _orchardStartups = orchardStartups;
            this.Logger = logger;
        }

        public async Task<IEnumerable<IViewDefinition>> HarvestAsync(IFeatureInfo feature) {
            var featureStartup = await this.GetOrDefaultS2StartupAsync(feature);
            if (featureStartup == null) {
                return new IViewDefinition[] { };
            }
            var viewFiles = new ViewDefinitionsCollection();
            featureStartup.ConfigureViews(viewFiles);
            var defs = new List<IViewDefinition>();
            foreach (var vf in viewFiles) {
                //读取文件
                var viewFilePath = Path.Combine("Areas", feature.Id, vf.Path);
                var viewFileInfo = _environment.ContentRootFileProvider.GetFileInfo(viewFilePath);
                if (!viewFileInfo.Exists) {
                    throw new FileNotFoundException($"File not found: '{viewFilePath}'", viewFilePath);
                }
                this.Logger.LogInformation($"View definition file found: {viewFilePath}");
                using (var vfs = viewFileInfo.CreateReadStream()) {
                    var viewDefinitions = await _xaml.LoadAsync<S2ViewDefinitions>(vfs);
                    foreach (var vd in viewDefinitions.Definitions) {
                        if (string.IsNullOrEmpty(vd.Feature)) {
                            vd.Feature = feature.Id;
                        }
                    }
                    defs.AddRange(viewDefinitions.Definitions);
                }
            }
            return defs;

        }

        private async Task<IS2Startup> GetOrDefaultS2StartupAsync(IFeatureInfo feature) {
            var s2StartupType = typeof(IS2Startup);
            var features = await _shellFeatureEntityService.GetEnabledFeatureEntriesAsync();
            var featureEntry = features.Single(x => x.FeatureInfo.Id == feature.Id);
            var startupType = featureEntry.ExportedTypes.SingleOrDefault(x => s2StartupType.IsAssignableFrom(x));
            return _orchardStartups.SingleOrDefault(x => x.GetType() == startupType) as IS2Startup;
        }

    }

}
