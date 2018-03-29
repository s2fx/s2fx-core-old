using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Remoting.Conventions {

    public class DefaultWampHostConvention : IWampHostConvention {
        public Uri GetHostingUri() => new Uri("ws://localhost:8081");
    }

}
