using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Data.Seeding {

    public class SeedLoader : ISeedLoader {

        public SeedLoader() {

        }

        public Task LoadAllSeedDataAsync(bool withDemoData = false) {
            throw new NotImplementedException();
        }

        public Task LoadSeedDataAsync(string feature, bool withDemoData = false) {
            throw new NotImplementedException();
        }

    }
}
