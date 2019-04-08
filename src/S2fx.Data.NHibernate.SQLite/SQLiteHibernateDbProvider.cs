using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Driver;
using S2fx.Data.NHibernate.Types;

namespace S2fx.Data.NHibernate.SQLite {

    public class SQLiteHibernateDbProvider : IHibernateDbProvider {
        public const string DbProviderName = "SQLite";

        public string Name => DbProviderName;

        public Type JsonObjectType => typeof(TextBasedJsonObjectType<>);

        public void SetupConfiguration(Configuration cfg) {
            cfg.SetProperty(global::NHibernate.Cfg.Environment.ConnectionDriver, typeof(global::NHibernate.Driver.SQLite20Driver).FullName);
            cfg.SetProperty(global::NHibernate.Cfg.Environment.Dialect, typeof(global::NHibernate.Dialect.SQLiteDialect).FullName);
        }

    }

}
