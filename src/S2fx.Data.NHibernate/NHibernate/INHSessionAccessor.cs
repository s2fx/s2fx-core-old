using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace S2fx.Data.NHibernate {

    public interface INHSessionAccessor {
        ISession Session { get; }
    }

}
