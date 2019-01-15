using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace S2fx.Data.NHibernate {

    public class HibernateDbMigrator : IDbMigrator {
        private readonly Configuration _cfg;
        public ILogger<HibernateDbMigrator> Logger { get; }

        public HibernateDbMigrator(Configuration cfg, ILogger<HibernateDbMigrator> logger) {
            _cfg = cfg;
            this.Logger = logger;
        }

        public async Task MigrateSchemeAsync() {

            this.Logger.LogInformation("Starting to migrate the schema of database...");
            var startTime = DateTime.UtcNow;
            var update = new SchemaUpdate(_cfg);
            await update.ExecuteAsync(false, true);
            var elapsedTime = DateTime.UtcNow - startTime;
            this.Logger.LogInformation("The schema of Database has been migrated successfully. Elapsed time: {0}", elapsedTime);
        }

    }

}
