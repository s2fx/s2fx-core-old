using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using S2fx.Environment.Configuration;

namespace S2fx.Data.NHibernate.DbProviders {

    public class HibernateDbProviderAccessor : IHibernateDbProviderAccessor {

        readonly IHibernateDbProvider _provider;

        public HibernateDbProviderAccessor(IEnumerable<IHibernateDbProvider> providers, S2Settings settings) {

            _provider = providers.SingleOrDefault(x => x.Name == settings.Db.Provider);
            if (_provider == null) {
                throw new NotSupportedException($"Not supported database provider: '{settings.Db.Provider}'");
            }
        }

        public IHibernateDbProvider EnabledDbProvider => _provider;

    }

}
