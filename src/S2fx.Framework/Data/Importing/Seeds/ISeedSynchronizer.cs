using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Data.Importing.Seeds {

    public interface ISeedSynchronizer {

        Task SynchronizeSeedAsync(string featureId, bool withDemoData = false);
        Task SynchronizeAllSeedsAsync(bool withDemoData = false);
    }

}
