using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public interface IMetadataContext {
        IReadOnlyDictionary<string, MetaEntity> Entities { get; }
    }

    public class MetadataContext : IMetadataContext {

        public IReadOnlyDictionary<string, MetaEntity> Entities { get; }

        public MetadataContext(IReadOnlyDictionary<string, MetaEntity> entities) {
            this.Entities = entities;
        }
    }

}
