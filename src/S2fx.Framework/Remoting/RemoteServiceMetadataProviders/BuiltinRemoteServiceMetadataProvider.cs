using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Extensions;
using OrchardCore.Environment.Extensions.Features;
using OrchardCore.Environment.Shell.Descriptor.Models;
using OrchardCore.Modules;
using S2fx.Environment.Shell;
using S2fx.Remoting.Model;

namespace S2fx.Remoting {

    public class BuiltinRemoteServiceMetadataProvider : AbstractClrTypeBasedRemoteServiceMetadataProvider {

        readonly IHttpContextAccessor _httpContextAccessor;
        readonly ITypeFeatureProvider _typeFeatureProvider;

        public BuiltinRemoteServiceMetadataProvider(
            IHttpContextAccessor httpContextAccessor, ITypeFeatureProvider typeFeatureProvider) {
            _httpContextAccessor = httpContextAccessor;
            _typeFeatureProvider = typeFeatureProvider;
        }

        public async override Task<IEnumerable<RemoteServiceInfo>> GetAllServicesAsync() {
            var services = _httpContextAccessor.HttpContext.RequestServices;
            var shellFeatureService = services.GetRequiredService<IShellFeatureEntityService>();
            var features = await shellFeatureService.GetEnabledFeatureEntriesAsync();
            var coreFeature = features.Single(x => x.FeatureInfo.Id == WellKnownConstants.CoreModuleId);
            var names = services.GetService<IServiceCollection>().Select(x => x.ServiceType.Name).OrderBy(x => x).ToArray();

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
