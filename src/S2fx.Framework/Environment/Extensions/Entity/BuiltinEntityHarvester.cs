using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Extensions;
using S2fx.Environment.Shell;

namespace S2fx.Environment.Extensions.Entity {

    public class BuiltinEntityHarvester : AbstractClrEntityHarvester {

        public BuiltinEntityHarvester(IServiceProvider services)
            : base(services) {
        }

        public override async Task<IEnumerable<EntityInfo>> HarvestEntitiesAsync() {

            var shellFeatureServiec = this.Services.GetRequiredService<IShellFeatureEntityService>();
            var features = await shellFeatureServiec.GetEnabledFeatureEntriesAsync();
            var coreFeature = features.Single(x => x.FeatureInfo.Id == WellKnownConstants.CoreModuleId);
            var builtinEntityTypes = Assembly.GetExecutingAssembly().ExportedTypes.Where(t => this.IsEntityType(t));

            var entityInfos = new List<EntityInfo>(builtinEntityTypes
                .Select(x => this.GetEntityInfo(coreFeature.FeatureInfo, x)));
            return entityInfos;
        }

    }


}
