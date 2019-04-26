using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Data.Importing.Seeds {

    public interface ISeedLoader {

        Task LoadSeedAsync(string featureId, bool withDemoData = false);
        Task LoadAllSeedsAsync(bool withDemoData = false);
    }

}
