using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Driver;
using S2fx.Data.NHibernate.Types;

namespace S2fx.Data.NHibernate.PostgreSQL {

    public class SQLiteHibernateDbProvider : IHibernateDbProvider {

        public string Name => "SQLite";

        public Type JsonObjectType => typeof(TextBasedJsonObjectType<>);

        public Type XmlObjectType => typeof(TextBasedXmlObjectType<>);

        public void SetupConfiguration(Configuration cfg) {
            cfg.SetProperty(global::NHibernate.Cfg.Environment.ConnectionDriver, typeof(SQLite20Driver).FullName);
            cfg.SetProperty(global::NHibernate.Cfg.Environment.Dialect, typeof(global::NHibernate.Dialect.SQLiteDialect).FullName);
        }

    }

}
