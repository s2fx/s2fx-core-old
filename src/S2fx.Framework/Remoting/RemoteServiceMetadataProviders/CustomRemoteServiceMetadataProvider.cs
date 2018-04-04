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

    public class CustomRemoteServiceMetadataProvider : AbstractClrTypeBasedRemoteServiceMetadataProvider {
        private readonly IShellFeatureService _shellFeatureService;
        private readonly IHostingEnvironment _environment;
        private readonly ITypeFeatureProvider _typeFeatureProvider;

        public CustomRemoteServiceMetadataProvider(
            IShellFeatureService shellFeatureService,
            IHostingEnvironment environment, ITypeFeatureProvider typeFeatureProvider) {

            _shellFeatureService = shellFeatureService;
            _environment = environment;
            _typeFeatureProvider = typeFeatureProvider;
        }

        public override async Task<IEnumerable<RemoteServiceInfo>> GetAllServicesAsync() {
            var enabledFeatures = await _shellFeatureService.GetEnabledFeatureEntriesAsync();
            var descriptors = new List<RemoteServiceInfo>();

            //User's Remote services
            foreach (var feature in enabledFeatures) {
                var serviceImplTypes = feature.ExportedTypes.Where(t => this.IsRemoteService(t));
                foreach (var serviceImplType in serviceImplTypes) {
                    var serviceMetadata = this.CreateServiceMetadata(feature.FeatureInfo, serviceImplType);
                    descriptors.Add(serviceMetadata);
                }
            }

            return descriptors;
        }


    }

}
