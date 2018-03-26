using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrchardCore.Environment.Extensions;
using S2fx.Remoting.Model;

namespace S2fx.Remoting {

    public class RemoteServiceManager : IRemoteServiceManager {
        private readonly IServiceProvider _services;
        private bool _loaded = false;
        public ILogger<RemoteServiceManager> Logger { get; }
        private object InitializationLock = new object();
        private readonly List<RemoteServiceInfo> _remoteServices = new List<RemoteServiceInfo>();

        public RemoteServiceManager(IServiceProvider services, ILogger<RemoteServiceManager> logger) {
            _services = services;
            Logger = logger;
        }

        public IReadOnlyList<RemoteServiceInfo> GetRemoteServices() {
            this.EnsureInitialized();
            return this._remoteServices;
        }

        public Task<IEnumerable<RemoteServiceInfo>> LoadRemoteServicesAsync() {
            this.EnsureInitialized();
            return Task.FromResult(this._remoteServices.AsEnumerable());
        }

        private void EnsureInitialized() {
            if (_loaded) {
                return;
            }
            var remoteServiceProviders = _services.GetServices<IRemoteServiceProvider>();
            var metadataProviders = _services.GetServices<IRemoteServiceMetadataProvider>();
            var typeFeatureProvider = _services.GetRequiredService<ITypeFeatureProvider>();
            lock (this.InitializationLock) {
                foreach (var metadataProvider in metadataProviders) {
                    var services = Task.Run(metadataProvider.GetAllServicesAsync).Result;
                    foreach (var s in services) {
                        this.Logger.LogInformation("Remote service found: [Feature={0}, Type={1}]", s.Feature.Id, s.Name);
                        _remoteServices.Add(s);

                        //Register the dynamic API controller type to Orchard's TypeFeatureProvider
                        //foreach
                        foreach (var rsp in remoteServiceProviders) {
                            var implType = rsp.MakeImplementationType(s);
                            typeFeatureProvider.TryAdd(implType, s.Feature);
                        }
                    }
                }
                _loaded = true;
            }
        }

    }
}
