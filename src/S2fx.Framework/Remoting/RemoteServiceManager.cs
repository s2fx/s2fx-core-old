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
        public ILogger Logger { get; }
        private object InitializationLock = new object();
        private readonly IDictionary<string, RemoteServiceInfo> _remoteServices = new Dictionary<string, RemoteServiceInfo>();

        public RemoteServiceManager(IServiceProvider services, ILogger<RemoteServiceManager> logger) {
            _services = services;
            Logger = logger;
        }

        public IEnumerable<RemoteServiceInfo> GetRemoteServices() {
            this.EnsureInitialized();
            return this._remoteServices.Values;
        }

        public RemoteServiceInfo GetRemoteService(string name) {
            this.EnsureInitialized();
            return _remoteServices[name];
        }

        public bool TryGetRemoteService(string name, out RemoteServiceInfo serviceInfo) {
            this.EnsureInitialized();
            return _remoteServices.TryGetValue(name, out serviceInfo);
        }

        public RemoteServiceInfo GetRemoteService(Type clrType) =>
            _remoteServices.Values.Single(x => x.ClrType == clrType);

        public bool TryGetRemoteService(Type clrType, out RemoteServiceInfo serviceInfo) {
            serviceInfo = _remoteServices.Values.SingleOrDefault(x => x.ClrType == clrType);
            return serviceInfo != null ? true : false;
        }

        public Task<IEnumerable<RemoteServiceInfo>> LoadRemoteServicesAsync() {
            this.EnsureInitialized();
            var result = this._remoteServices.Values as IEnumerable<RemoteServiceInfo>;
            return Task.FromResult(result);
        }

        private void EnsureInitialized() {
            if (_loaded) {
                return;
            }
            var remoteServiceProviders = _services.GetServices<IRemoteServiceProvider>();
            var metadataProviders = _services.GetServices<IRemoteServiceMetadataProvider>();
            lock (this.InitializationLock) {
                foreach (var metadataProvider in metadataProviders) {
                    var services = Task.Run(metadataProvider.GetAllServicesAsync).Result;
                    foreach (var s in services) {
                        this.Logger.LogInformation("Remote service found: [Feature={0}, Type={1}, Provider={2}]",
                            s.Feature.Id, s.Name, metadataProvider.GetType().FullName);
                        _remoteServices.Add(s.Name, s);

                        //this.TryRegisterRemoteServiceProxyType(remoteServiceProviders, s);
                    }
                }
                _loaded = true;
            }
        }

        private void TryRegisterRemoteServiceProxyType(IEnumerable<IRemoteServiceProvider> remoteServiceProviders, RemoteServiceInfo s) {
            //Register the dynamic API controller type to Orchard's TypeFeatureProvider
            //foreach
            var typeFeatureProvider = _services.GetRequiredService<ITypeFeatureProvider>();
            foreach (var rsp in remoteServiceProviders) {
                if (rsp.IsRemoteServiceProxyTypeRequired) {
                    var implType = rsp.MakeRemoteServiceProxyType(s);
                    typeFeatureProvider.TryAdd(implType, s.Feature);
                }
            }
        }
    }
}
