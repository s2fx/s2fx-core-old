using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Driver;

namespace S2fx.Data.NHibernate.PostgreSQL {

    public class SQLiteHibernateDbProvider : IHibernateDbProvider {

        public string Name => "SQLite";

        public void SetupConfiguration(Configuration cfg) {
            cfg.SetProperty(global::NHibernate.Cfg.Environment.ConnectionDriver, typeof(SQLite20Driver).FullName);
            cfg.SetProperty(global::NHibernate.Cfg.Environment.Dialect, typeof(global::NHibernate.Dialect.SQLiteDialect).FullName);
        }

    }

}
