using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Remoting;
using S2fx.Remoting.Model;

namespace S2fx.Web.Remoting {

    public class MvcControllerRemoteServiceProvider : IRemoteServiceProvider {
        public string Name => "AspNetCoreController";

        public Type MakeImplementationType(RemoteServiceInfo service) {
            return service.ClrType;
        }
    }
}
