using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Environment.Extensions.Features;
using S2fx.Model.Metadata;

namespace S2fx.Environment.Extensions.Entity {

    public class FeatureEntities {
        public string Feature { get; }
        public IEnumerable<EntityInfo> Entities { get; }

        public FeatureEntities(string feature, IEnumerable<EntityInfo> entities) {
            this.Feature = feature;
            this.Entities = entities;
        }
    }

}
