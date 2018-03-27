using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;

namespace S2fx.Data.Importing.Model {

    public class ImportContext {

        public Guid Id { get; } = Guid.NewGuid();
        public string Feature { get; }
        public MetaEntity Entity { get; }
        public IList<PropertyBinder> PropertyBinders { get; } = new List<PropertyBinder>();
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();
        public IEnumerable<Guid> Dependencies { get; }
        public bool CanUpdate { get; }

        public ImportContext(
            string feature,
            MetaEntity entity,
            bool canUpdate = false,
            IEnumerable<Guid> dependencies = null) {

            this.Feature = feature ?? throw new ArgumentNullException(nameof(entity));
            this.Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            this.CanUpdate = canUpdate;
            this.Dependencies = dependencies ?? new Guid[] { };
        }

    }

}
