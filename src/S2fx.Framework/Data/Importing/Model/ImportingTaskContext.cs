using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;

namespace S2fx.Data.Importing.Model {

    public class ImportingTaskContext {

        public Guid Id { get; } = Guid.NewGuid();
        public string Feature { get; }
        public MetaEntity Entity { get; }
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();
        public IEnumerable<Guid> Dependencies { get; }
        public EntityMapping EntityBinding { get; }

        public ImportingTaskContext(
            string feature,
            MetaEntity entity,
            EntityMapping entityBinding,
            IEnumerable<Guid> dependencies = null) {

            this.Feature = feature ?? throw new ArgumentNullException(nameof(entity));
            this.Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            this.EntityBinding = entityBinding ?? throw new ArgumentNullException(nameof(entity));
            this.Dependencies = dependencies ?? new Guid[] { };
        }

    }

}
