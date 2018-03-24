using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.Remoting.Model;

namespace S2fx.Remoting {

    public interface IRemoteServiceManager {
        Task<IEnumerable<RemoteServiceInfo>> LoadRemoteServicesAsync();
        IReadOnlyList<RemoteServiceInfo> GetRemoteServices();
    }

}
