using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Extensions;

namespace S2fx.Environment.Extensions.Entity {

    public class BuiltinEntityHarvester : AbstractClrEntityHarvester {

        public BuiltinEntityHarvester(IServiceProvider services)
            : base(services) {
        }

        public override async Task<IEnumerable<EntityInfo>> HarvestEntitiesAsync() {

            var extensions = this.Services.GetRequiredService<IExtensionManager>();
            var allFeatures = await extensions.LoadFeaturesAsync();
            var coreFeature = allFeatures.Single(x => x.FeatureInfo.Id == WellKnownConstants.CoreModuleId);
            var builtinEntityTypes = Assembly.GetExecutingAssembly().ExportedTypes.Where(t => this.IsEntityType(t));

            var entityInfos = new List<EntityInfo>(builtinEntityTypes
                .Select(x => this.GetEntityInfo(coreFeature.FeatureInfo, x)));
            return entityInfos;
        }

    }


}
