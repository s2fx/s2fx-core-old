using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using S2fx.Environment.Configuration;

namespace S2fx.Data.NHibernate.DbProviders {

    public class HibernateDbProviderAccessor : IHibernateDbProviderAccessor {

        readonly IHibernateDbProvider _provider;

        public HibernateDbProviderAccessor(IEnumerable<IDbProvider> providers, S2Settings settings) {

            var provider = providers.SingleOrDefault(x => x.Name == settings.Db.Provider);
            if (provider == null) {
                throw new NotSupportedException($"Not supported database provider: '{settings.Db.Provider}'");
            }
            _provider = (IHibernateDbProvider)provider;
        }

        public IHibernateDbProvider EnabledDbProvider => _provider;

    }

}
