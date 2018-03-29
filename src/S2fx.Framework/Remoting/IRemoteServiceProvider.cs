using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Remoting.Model;

namespace S2fx.Remoting {

    public interface IRemoteServiceProvider {

        string Name { get; }
        bool IsRemoteServiceProxyTypeRequired { get; }
        Type MakeRemoteServiceProxyType(RemoteServiceInfo service);

    }

}
