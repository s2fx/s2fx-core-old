using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Data.Seeding {

    public interface ISeedDataLoader {

        Task LoadSeedDataAsync(string featureId, bool withDemoData = false);
        Task LoadAllSeedDataAsync(bool withDemoData = false);
    }

}
