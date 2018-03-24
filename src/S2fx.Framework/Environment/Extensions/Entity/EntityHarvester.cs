using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Descriptor;
using OrchardCore.Modules;
using OrchardCore.Environment.Shell.Descriptor.Models;
using OrchardCore.Environment.Extensions;
using System.Reflection;
using S2fx.Model.Annotations;
using S2fx.Model.Metadata.Types;

namespace S2fx.Environment.Extensions.Entity {

    public class EntityHarvester : IEntityHarvester {
        private readonly IServiceProvider _services;
        //private readonly IShellDescriptorManager _shellDescriptorManager;
        private readonly IHostingEnvironment _environment;

        public EntityHarvester(IServiceProvider services,
                               //IShellDescriptorManager shellDescriptorManager,
                               IHostingEnvironment environment) {
            _services = services;
            //_shellDescriptorManager = shellDescriptorManager;
        }

        public async Task<IEnumerable<EntityInfo>> HarvestEntitiesAsync() {
            //var shell = await _shellDescriptorManager.GetShellDescriptorAsync();
            var extensions = _services.GetRequiredService<IExtensionManager>();
            var allFeatures = await extensions.LoadFeaturesAsync();
            var allEntities = new List<EntityInfo>();
            /*
            var clrEntityType = _services.GetServices<IEntityType>().Single(x => x.Name == BuiltinEntityTypeNames.ClrSqlEntityTypeName);
            foreach (var feature in allFeatures) {
                var entityTypes = feature.ExportedTypes.Where(t => this.IsEntityType(t));
                foreach (var entityType in entityTypes) {
                    var entityAttr = entityType.GetCustomAttribute<EntityAttribute>();
                    var ed = new EntityDescriptor(feature.FeatureInfo, entityAttr.Name, clrEntityType, entityType);
                }
            }
            */
            return allEntities;
        }

        private bool IsEntityType(Type t) =>
            t.IsClass && !t.IsAbstract && t.GetCustomAttribute<EntityAttribute>() != null;

    }

}
