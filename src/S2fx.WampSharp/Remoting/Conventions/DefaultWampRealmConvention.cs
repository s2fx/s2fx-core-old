using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Remoting.Conventions {

    public class DefaultWampRealmConvention : IWampRealmConvention {

        public string GetS2WampRealmName() => "s2";

    }
}
