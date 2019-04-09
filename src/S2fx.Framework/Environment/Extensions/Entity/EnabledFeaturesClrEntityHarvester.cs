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
using S2fx.Environment.Shell;

namespace S2fx.Environment.Extensions.Entity {

    public class EnabledFeaturesClrEntityHarvester : AbstractClrEntityHarvester {

        private readonly IShellFeatureEntityService _shellFeatureEntityService;

        public override int Priority => -1000;

        public EnabledFeaturesClrEntityHarvester(IShellFeatureEntityService shellFeatureEntityService) {
            _shellFeatureEntityService = shellFeatureEntityService;
        }

        public override async Task<IEnumerable<EntityInfo>> HarvestEntitiesAsync() {
            var features = await _shellFeatureEntityService.GetEnabledFeatureEntriesAsync();
            var allEntities = features.SelectMany(x => this.HarvestClrEntityInFeature(x));
            return allEntities;
        }

    }

}
