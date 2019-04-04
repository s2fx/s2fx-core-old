using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Driver;

namespace S2fx.Data.NHibernate.Npgsql {

    public class PostgreSQLHibernateDbProvider : IHibernateDbProvider {
        public const string DbProviderName = "PostgreSQL";

        public string Name => DbProviderName;

        public void SetupConfiguration(Configuration cfg) {
            cfg.SetProperty(global::NHibernate.Cfg.Environment.ConnectionDriver, typeof(NpgsqlDriver).FullName);
            cfg.SetProperty(global::NHibernate.Cfg.Environment.Dialect, typeof(global::NHibernate.Dialect.PostgreSQL83Dialect).FullName);
        }

    }

}
