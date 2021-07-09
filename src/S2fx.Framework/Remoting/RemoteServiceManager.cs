using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrchardCore.Environment.Extensions;
using OrchardCore.Environment.Shell;
using S2fx.Remoting.Model;

namespace S2fx.Remoting {

    public class RemoteServiceManager : IRemoteServiceManager {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly SemaphoreSlim _initLock = new SemaphoreSlim(1);
        readonly IDictionary<string, RemoteServiceInfo> _remoteServices = new Dictionary<string, RemoteServiceInfo>();
        bool _isLoaded = false;

        public ILogger Logger { get; }

        public bool IsLoaded => _isLoaded;

        public RemoteServiceManager(IHttpContextAccessor httpContextAccessor, ILogger<RemoteServiceManager> logger) {
            _httpContextAccessor = httpContextAccessor;
            Logger = logger;
        }

        public async Task InitializeAsync() {
            if (_isLoaded) {
                return;
            }
            var services = _httpContextAccessor.HttpContext.RequestServices;
            var remoteServiceProviders = services.GetServices<IRemoteServiceProvider>();
            var metadataProviders = services.GetServices<IRemoteServiceMetadataProvider>();
            await this._initLock.WaitAsync();
            try {
                foreach (var metadataProvider in metadataProviders) {
                    var remoteServices = await metadataProvider.GetAllServicesAsync();
                    foreach (var rs in remoteServices) {
                        if (this.Logger.IsEnabled(LogLevel.Debug)) {
                            this.Logger.LogDebug("Remote service found: [Feature={0}, Type={1}, Provider={2}]",
                                rs.Feature.Id, rs.Name, metadataProvider.GetType().FullName);
                        }
                        _remoteServices.Add(rs.Name, rs);

                        this.TryRegisterRemoteServiceProxyType(services, remoteServiceProviders, rs);
                    }
                }
                _isLoaded = true;
            }
            finally {
                this._initLock.Release();
            }
        }

        public IEnumerable<RemoteServiceInfo> GetRemoteServices() {
            if (!_isLoaded) {
                throw new InvalidOperationException("Not initialized");
            }
            return _remoteServices.Values;
        }

        public RemoteServiceInfo GetRemoteService(string name) {
            if (!_isLoaded) {
                throw new InvalidOperationException("Not initialized");
            }
            return _remoteServices[name];
        }

        public bool TryGetRemoteService(string name, out RemoteServiceInfo serviceInfo) {
            if (!_isLoaded) {
                throw new InvalidOperationException("Not initialized");
            }
            return _remoteServices.TryGetValue(name, out serviceInfo);
        }

        public RemoteServiceInfo GetRemoteService(Type clrType) {
            if (!_isLoaded) {
                throw new InvalidOperationException("Not initialized");
            }
            return _remoteServices.Values.Single(x => x.ClrType == clrType);
        }

        public bool TryGetRemoteService(Type clrType, out RemoteServiceInfo serviceInfo) {
            if (!_isLoaded) {
                throw new InvalidOperationException("Not initialized");
            }
            serviceInfo = _remoteServices.Values.SingleOrDefault(x => x.ClrType == clrType);
            return serviceInfo != null ? true : false;
        }

        public async Task<IEnumerable<RemoteServiceInfo>> LoadRemoteServicesAsync() {
            if (!_isLoaded) {
                throw new InvalidOperationException("Not initialized");
            }
            await Task.CompletedTask;
            return _remoteServices.Values;
        }


        private void TryRegisterRemoteServiceProxyType(
            IServiceProvider sp, IEnumerable<IRemoteServiceProvider> remoteServiceProviders, RemoteServiceInfo s) {
            //Register the dynamic API controller type to Orchard's TypeFeatureProvider
            //foreach
            var typeFeatureProvider = sp.GetRequiredService<ITypeFeatureProvider>();
            foreach (var rsp in remoteServiceProviders) {
                if (rsp.IsRemoteServiceProxyTypeRequired) {
                    var implType = rsp.MakeRemoteServiceProxyType(s);
                    typeFeatureProvider.TryAdd(implType, s.Feature);
                }
            }
        }

    }

}
