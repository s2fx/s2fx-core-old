using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;

namespace S2fx.Data.NHibernate {

    public interface INhDbProvider {

        void SetupConfiguration(Configuration cfg);

    }

}
