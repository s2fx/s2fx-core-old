using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;

namespace S2fx.Data.NHibernate {

    public interface IHibernateDbProvider {
        string Name { get; }

        void SetupConfiguration(Configuration cfg);

        Type JsonObjectType { get; }

        Type XmlObjectType { get; }

    }

}
