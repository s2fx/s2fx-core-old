using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using S2fx.Data.Importing.Model;

namespace S2fx.Data.Importing {

    public interface ISeedManifestCollection : 
        IList<SeedManifestDescriptor>, 
        ICollection<SeedManifestDescriptor>, 
        IEnumerable<SeedManifestDescriptor>, 
        IEnumerable {
    }
}
