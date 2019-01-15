using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.Environment.Extensions.Features;

namespace S2fx.Environment.Shell {

    public interface IShellFeatureEntityService {
        Task<IEnumerable<FeatureEntry>> GetEnabledFeatureEntriesAsync();
    }

}
