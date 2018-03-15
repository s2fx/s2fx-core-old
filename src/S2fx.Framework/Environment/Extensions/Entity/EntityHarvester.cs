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
        private readonly IShellDescriptorManager _shellDescriptorManager;
        private readonly IHostingEnvironment _environment;

        public EntityHarvester(IEnumerable<IEntityMetadataProvider> providers,
                               IShellDescriptorManager shellDescriptorManager,
                               IHostingEnvironment environment) {
            _providers = providers;
            _shellDescriptorManager = shellDescriptorManager;
            _environment = environment;
        }

        public async Task<IEnumerable<FeatureEntities>> HarvestEntitiesAsync() {
            var shell = await _shellDescriptorManager.GetShellDescriptorAsync();

            var featureEntities = new Dictionary<string, IEnumerable<EntityInfo>>();
            foreach (var provider in _providers) {
                foreach (var moduleName in _environment.GetApplication().ModuleNames) {
                    var entities = provider.GetEntitiesMetadata(moduleName);
                    if (featureEntities.TryGetValue(moduleName, out var oldEntities)) {
                        featureEntities[moduleName] = oldEntities.Union(entities);
                    }
                    else {
                        featureEntities.Add(moduleName, entities);
                    }
                }
            }

            var enabledFeatureIds = new HashSet<string>(shell.Features.Select(x => x.Id));

            return featureEntities.Where(x => enabledFeatureIds.Contains(x.Key))
                .Select(x => new FeatureEntities(x.Key, x.Value))
                .ToArray();
        }
    }

}
