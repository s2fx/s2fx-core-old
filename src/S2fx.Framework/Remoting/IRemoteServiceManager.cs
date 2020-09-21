using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.Remoting.Model;

namespace S2fx.Remoting {

    public interface IRemoteServiceManager {
        bool IsLoaded { get; }
        Task InitializedAsync();
        Task<IEnumerable<RemoteServiceInfo>> LoadRemoteServicesAsync();
        IEnumerable<RemoteServiceInfo> GetRemoteServices();
        RemoteServiceInfo GetRemoteService(string name);
        bool TryGetRemoteService(string name, out RemoteServiceInfo serviceInfo);
        RemoteServiceInfo GetRemoteService(Type clrType);
        bool TryGetRemoteService(Type clrType, out RemoteServiceInfo serviceInfo);
    }

}
