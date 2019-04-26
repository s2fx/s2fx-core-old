using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using OrchardCore.Environment.Extensions.Features;
using S2fx.Data.Importing.Model;
using S2fx.View.Schemas;
using S2fx.Xaml;

namespace S2fx.View.Data {

    public class ModularViewDataHarvester : IViewDataHarvester {
        public const string ViewsManifestFileName = "ViewsManifest.xaml";

        readonly IHostingEnvironment _environment;
        readonly IXamlService _xaml;

        public ModularViewDataHarvester(IHostingEnvironment environment, IXamlService xaml) {
            _environment = environment;
            _xaml = xaml;
        }

        public async Task<IEnumerable<IViewDefinition>> HarvestAsync(IFeatureInfo feature) {
            var manifestPath = Path.Combine("Areas", feature.Id, ViewsManifestFileName);
            var manifestFile = _environment.ContentRootFileProvider.GetFileInfo(manifestPath);
            if (manifestFile == null || manifestFile is NotFoundFileInfo) {
                return new IViewDefinition[] { };
            }

            using (var stream = manifestFile.CreateReadStream()) {
                var manifest = await _xaml.LoadAsync<ViewsManifest>(stream);
                var defs = new List<IViewDefinition>();
                foreach (var vf in manifest.ViewFiles) {
                    //读取文件
                    var viewFilePath = Path.Combine("Areas", feature.Id, vf.File);
                    var viewFileInfo = _environment.ContentRootFileProvider.GetFileInfo(viewFilePath);
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

        }

    }

}
