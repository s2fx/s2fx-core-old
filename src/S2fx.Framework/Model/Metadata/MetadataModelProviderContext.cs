using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Environment;

namespace S2fx.Model.Metadata {

    public class MetadataModelProviderContext {

        public IEnumerable<EntityEntry> EntityEntries { get; }

        public MetadataModelProviderContext(IEnumerable<EntityEntry> entries) {
            this.EntityEntries = entries;
        }

        public MetadataModel Result { get; } = new MetadataModel();

    }

}
