using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrchardCore.Environment.Extensions.Features;
using S2fx.Model.Metadata.Types;

namespace S2fx.Model.Environment {

    public sealed class EntityEntry {
        public IFeatureInfo Feature { get; set; }
        public string Name { get; set; }
        public Type ClrType { get; set; }
        public string EntityType { get; set; }
        public IEnumerable<Attribute> Attributes { get; set; }
    }

}
