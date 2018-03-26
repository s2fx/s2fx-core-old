using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;

namespace S2fx.Data.Importing.Model {

    public class ImportJob {

        public Guid Id { get; } = Guid.NewGuid();
        public string Feature { get; }
        public MetaEntity Entity { get; }
        public EntityBinder Bind { get; }
        public DataSourceInfo DataSource { get; }
        public IEnumerable<Guid> Dependencies { get; }
        public bool CanUpdate { get; }

        public ImportJob(
            string feature,
            MetaEntity entity,
            EntityBinder bind, DataSourceInfo dataSource,
            bool canUpdate = false,
            IEnumerable<Guid> dependencies = null) {

            this.Feature = feature ?? throw new ArgumentNullException(nameof(entity));
            this.Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            this.Bind = bind ?? throw new ArgumentNullException(nameof(bind));
            this.DataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
            this.CanUpdate = canUpdate;
            this.Dependencies = dependencies ?? new Guid[] { };
        }

    }

}
