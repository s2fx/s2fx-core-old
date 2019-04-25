using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using OrchardCore.Modules;

namespace S2fx.Data.NHibernate {

    public class HibernateDbMigrator : IDbMigrator {
        readonly Configuration _cfg;
        readonly IClock _clock;
        readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);


        public ILogger<HibernateDbMigrator> Logger { get; }

        public HibernateDbMigrator(Configuration cfg, IClock clock, ILogger<HibernateDbMigrator> logger) {
            _cfg = cfg;
            _clock = clock;
            this.Logger = logger;
        }

        public async Task MigrateSchemaAsync() {
            //Lock the Migration processing
            await _semaphoreSlim.WaitAsync();
            try {
                this.Logger.LogInformation("Starting to migrate the schema of database...");
                var startTime = _clock.UtcNow;
                var update = new SchemaUpdate(_cfg);
                await update.ExecuteAsync(false, true);
                var elapsedTime = _clock.UtcNow - startTime;
                this.Logger.LogInformation("The schema of Database has been migrated successfully. Elapsed time: {0}", elapsedTime.ToString());
            }
            finally {
                _semaphoreSlim.Release();
            }
        }

    }

}
