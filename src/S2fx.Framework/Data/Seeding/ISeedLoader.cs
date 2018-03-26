using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Data.Seeding {

    public interface ISeedLoader {

        Task LoadSeedDataAsync(string feature, bool withDemoData = false);
        Task LoadAllSeedDataAsync(bool withDemoData = false);
    }

}
