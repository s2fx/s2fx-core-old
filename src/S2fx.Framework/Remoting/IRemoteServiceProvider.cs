using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Remoting.Model;

namespace S2fx.Remoting {

    public interface IRemoteServiceProvider {

        string Name { get; }
        Type MakeImplementationType(RemoteServiceInfo service);

    }

}
