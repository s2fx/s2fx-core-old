using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Descriptor;
using OrchardCore.Modules;
using OrchardCore.Environment.Shell.Descriptor.Models;

namespace S2fx.Environment.Extensions.Entity {

    public class EntityHarvester : IEntityHarvester {
        private readonly IEnumerable<IModuleEntityInspector> _inspectors;
        //private readonly IShellDescriptorManager _shellDescriptorManager;
        private readonly IHostingEnvironment _environment;

        public EntityHarvester(IEnumerable<IModuleEntityInspector> inspectors,
                               //IShellDescriptorManager shellDescriptorManager,
                               IHostingEnvironment environment) {
            _inspectors = inspectors;
            //_shellDescriptorManager = shellDescriptorManager;
            _environment = environment;
        }

        public async Task<IEnumerable<EntityDescriptor>> HarvestEntitiesAsync() {
            //var shell = await _shellDescriptorManager.GetShellDescriptorAsync();
            var modules = _environment.GetApplication().ModuleNames
                    .Select(m => _environment.GetModule(m));

            var allEntities = new List<EntityDescriptor>();
            foreach (var module in modules) {
                var entities = await this.HarvestEntitiesInModuleAsync(module);
                allEntities.AddRange(entities);
            }
            return allEntities;
        }

        private async Task<IEnumerable<EntityDescriptor>> HarvestEntitiesInModuleAsync(Module orchardModule) {
            var entities = new List<EntityDescriptor>();
            foreach (var provider in _inspectors) {
                var entitiesInModule = await provider.InspectEntitiesAsync(orchardModule);
                entities.AddRange(entitiesInModule);
            }
            return entities;
        }

    }

}
