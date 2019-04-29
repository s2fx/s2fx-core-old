using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.Environment.Extensions.Features;
using S2fx.Data.Importing.Model;
using S2fx.View.Schemas;

namespace S2fx.View.Data {

    public interface IViewHarvester {
        Task<IEnumerable<IViewDefinition>> HarvestAsync(IFeatureInfo feature);
    }

}
