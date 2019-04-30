using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Data.Importing.Schemas;

namespace S2fx.Data.Importing {

    public static class SeedManifestCollectionExtensions {

        public static ISeedManifestCollection AddManifestFile(this ISeedManifestCollection self, string path) {
            self.Add(new Model.SeedManifestDescriptor(path));
            return self;
        }

    }

}
