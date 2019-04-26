using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using S2fx.Environment.Configuration;

namespace S2fx.Data.NHibernate.DbProviders {

    public class HibernateDbProviderAccessor : IHibernateDbProviderAccessor {

        readonly IHibernateDbProvider _provider;

        public HibernateDbProviderAccessor(IEnumerable<IDbProvider> providers, S2AppSettings appSettings) {

            var provider = providers.SingleOrDefault(x => x.Name == appSettings.DbProvider);
            if (provider == null) {
                throw new NotSupportedException($"Not supported database provider: '{appSettings.DbProvider}'");
            }
            _provider = (IHibernateDbProvider)provider;
        }

        public IHibernateDbProvider EnabledDbProvider => _provider;

    }

}
