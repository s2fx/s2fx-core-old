using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Remoting.Model;

namespace S2fx.Remoting {

    public class WampRemoteServiceProvider : IRemoteServiceProvider {
        public string Name => "WAMP";

        public bool IsRemoteServiceProxyTypeRequired => false;

        public Type MakeRemoteServiceProxyType(RemoteServiceInfo service) {
            throw new NotSupportedException();
        }

    }

}
