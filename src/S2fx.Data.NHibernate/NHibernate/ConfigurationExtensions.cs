using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;

namespace S2fx.Data.NHibernate {

    public static class ConfigurationExtensions {

        public static Configuration SetConnectionString(this Configuration cfg, string connectionString) {
            if (cfg == null) {
                throw new ArgumentNullException(nameof(cfg));
            }
            cfg.SetProperty(global::NHibernate.Cfg.Environment.ConnectionString, connectionString);
            return cfg;
        }

        public static Configuration UseNpgsql(this Configuration cfg) {
            if (cfg == null) {
                throw new ArgumentNullException(nameof(cfg));
            }
            cfg.SetProperty(global::NHibernate.Cfg.Environment.ConnectionDriver, typeof(global::NHibernate.Driver.NpgsqlDriver).FullName);
            cfg.SetProperty(global::NHibernate.Cfg.Environment.Dialect, typeof(global::NHibernate.Dialect.PostgreSQL83Dialect).FullName);
            return cfg;
        }

    }

}
