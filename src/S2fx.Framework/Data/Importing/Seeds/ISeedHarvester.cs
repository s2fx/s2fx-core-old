using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.Environment.Extensions.Features;
using S2fx.Data.Importing.Model;

namespace S2fx.Data.Importing.Seeds {

    public interface ISeedHarvester {

        Task<IEnumerable<ImportingTaskDescriptor>> HarvestInitDataAsync(IFeatureInfo feature);

        Task<IEnumerable<ImportingTaskDescriptor>> HarvestDemoDataAsync(IFeatureInfo feature);

    }

}
