using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.Remoting.Model;

namespace S2fx.Environment.Extensions.Remoting {

    public interface IRemoteServiceMetadataProvider {
        Task<IEnumerable<RemoteServiceInfo>> GetAllServicesAsync();
    }

}
