using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrchardCore.Environment.Extensions.Features;
using S2fx.Model.Metadata.Types;

namespace S2fx.Environment.Extensions.Entity {

    public class EntityInfo {
        public IFeatureInfo Feature { get; }
        public string Name { get; }
        public IEntityType Type { get; }
        public Type ClrType { get; }

        public EntityInfo(IFeatureInfo feature, string name, IEntityType type, Type clrType) {
            this.Feature = feature;
            this.Name = name;
            this.Type = type;
            this.ClrType = clrType;
        }
    }

}
