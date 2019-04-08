using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.NHibernate.DbProviders {

    public interface IHibernateDbProviderAccessor {

        IHibernateDbProvider EnabledDbProvider { get; }

    }

}
