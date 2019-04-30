using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Importing.Model {

    public class SeedManifestDescriptor {
        public string Path { get; }

        public SeedManifestDescriptor(string path) {
            this.Path = path;
        }
    }

}
