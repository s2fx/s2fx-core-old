using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Descriptor;
using OrchardCore.Modules;
using S2fx.Model.Metadata;
using OrchardCore.Environment.Shell.Descriptor.Models;

namespace S2fx.Environment.Extensions.Entity {

    public class EntityHarvester : IEntityHarvester {
        private readonly IEnumerable<IEntityMetadataProvider> _providers;
        //private readonly IShellDescriptorManager _shellDescriptorManager;
        private readonly IHostingEnvironment _environment;

        public EntityHarvester(IEnumerable<IEntityMetadataProvider> providers,
                               //IShellDescriptorManager shellDescriptorManager,
                               IHostingEnvironment environment) {
            _providers = providers;
            //_shellDescriptorManager = shellDescriptorManager;
            _environment = environment;
        }

        public async Task<IEnumerable<FeatureEntities>> HarvestEntitiesAsync() {
            //var shell = await _shellDescriptorManager.GetShellDescriptorAsync();
            var modules = _environment.GetApplication().ModuleNames.Select(m => _environment.GetModule(m));

            var featureEntities = new Dictionary<string, IEnumerable<MetaEntity>>();
            foreach (var provider in _providers) {
                foreach (var module in modules) {
                    var entities = provider.GetEntitiesMetadata(module.Name);
                    if (module.ModuleInfo is S2ModuleAttribute moduleAttr) {
                        var s2ModuleName = moduleAttr.Name;
                        if (featureEntities.TryGetValue(s2ModuleName, out var oldEntities)) {
                            featureEntities[s2ModuleName] = oldEntities.Union(entities);
                        }
                        else {
                            featureEntities.Add(s2ModuleName, entities);
                        }
                    }
                }
            }

            /*
            var enabledFeatureIds = new HashSet<string>(_)
                //new HashSet<string>(shell.Features.Select(x => _environment.GetModule(x.Id).ModuleInfo.Name));

            featureEntities.Where(x => enabledFeatureIds.Contains(x.Key))
                .Select(x => new FeatureEntities(x.Key, x.Value))
                .ToArray();
                */
            return featureEntities.Select(x => new FeatureEntities(x.Key, x.Value))
                .ToArray();
        }
    }

}
