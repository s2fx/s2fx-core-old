using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public class MetaEntity : AbstractMetadata {

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public Type ClrType { get; set; }

        public IDictionary<string, MetaProperty> Properties { get; } = new Dictionary<string, MetaProperty>();
    }

}
