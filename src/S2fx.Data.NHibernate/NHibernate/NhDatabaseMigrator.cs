using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace S2fx.Data.NHibernate {

    public class NhDatabaseMigrator : IDatabaseMigrator {
        private readonly Configuration _cfg;

        public NhDatabaseMigrator(Configuration cfg) {
            _cfg = cfg;
        }

        public async Task MigrateSchemeAsync() {
            var update = new SchemaUpdate(_cfg);
            await update.ExecuteAsync(false, true);
        }

    }

}
