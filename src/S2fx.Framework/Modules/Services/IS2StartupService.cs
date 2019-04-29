using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.Environment.Extensions.Features;

namespace S2fx.Modules.Services {

    public interface IS2StartupService {
        Task<IS2Startup> GetOrDefaultByFeatureAsync(IFeatureInfo feature);
    }

}
