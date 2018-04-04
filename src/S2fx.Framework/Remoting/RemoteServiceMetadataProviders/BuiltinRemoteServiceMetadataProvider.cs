using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Extensions;
using OrchardCore.Environment.Extensions.Features;
using OrchardCore.Environment.Shell.Descriptor.Models;
using OrchardCore.Modules;
using S2fx.Environment.Shell;
using S2fx.Remoting.Model;

namespace S2fx.Remoting {

    public class BuiltinRemoteServiceMetadataProvider : AbstractClrTypeBasedRemoteServiceMetadataProvider {

        private readonly IServiceProvider _services;
        private readonly ITypeFeatureProvider _typeFeatureProvider;

        public BuiltinRemoteServiceMetadataProvider(
            IServiceProvider services, ITypeFeatureProvider typeFeatureProvider) {
            _services = services;
            _typeFeatureProvider = typeFeatureProvider;
        }

        public async override Task<IEnumerable<RemoteServiceInfo>> GetAllServicesAsync() {
            var shellFeatureService = _services.GetRequiredService<IShellFeatureService>();
            var features = await shellFeatureService.GetEnabledFeatureEntriesAsync();
            var coreFeature = features.Single(x => x.FeatureInfo.Id == WellKnownConstants.CoreModuleId);
            var names = _services.GetService<IServiceCollection>().Select(x => x.ServiceType.Name).OrderBy(x => x).ToArray();

            var serviceTypes = Assembly.GetExecutingAssembly().ExportedTypes;
            var remoteServices = serviceTypes
                  .Where(x => this.IsRemoteService(x))
                  .Select(x => {
                      //_typeFeatureProvider.TryAdd(x, coreFeature.FeatureInfo);
                      return this.CreateServiceMetadata(coreFeature.FeatureInfo, x);
                  });

            return remoteServices;
        }


    }

}
