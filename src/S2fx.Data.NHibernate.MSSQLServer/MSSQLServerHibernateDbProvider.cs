using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Driver;

namespace S2fx.Data.NHibernate.MSSQLServer {

    public class MSSQLServerHibernateDbProvider : IHibernateDbProvider {
        public const string DbProviderName = "SQLServer";

        public string Name => DbProviderName;

        public void SetupConfiguration(Configuration cfg) {
            cfg.SetProperty(global::NHibernate.Cfg.Environment.ConnectionDriver, typeof(global::NHibernate.Driver.SqlClientDriver).FullName);
            cfg.SetProperty(global::NHibernate.Cfg.Environment.Dialect, typeof(global::NHibernate.Dialect.MsSql2012Dialect).FullName);
        }

    }

}
