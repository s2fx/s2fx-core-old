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
using OrchardCore.Modules;
using S2fx.Remoting;
using S2fx.Remoting.Model;

namespace S2fx.Environment.Extensions.Remoting {

    public class ModuleAssemblyRemoteServiceMetadataProvider : IRemoteServiceMetadataProvider {
        private readonly IExtensionManager _extensions;
        private readonly IHostingEnvironment _environment;
        private readonly ITypeFeatureProvider _typeFeatureProvider;

        public ModuleAssemblyRemoteServiceMetadataProvider(IExtensionManager extensions, IHostingEnvironment environment, ITypeFeatureProvider typeFeatureProvider) {
            _extensions = extensions;
            _environment = environment;
            _typeFeatureProvider = typeFeatureProvider;
        }

        public async Task<IEnumerable<RemoteServiceInfo>> GetAllServicesAsync() {
            var modules = _environment.GetApplication().ModuleNames
               .Select(m => _environment.GetModule(m));
            var descriptors = new List<RemoteServiceInfo>();
            var allFeatures = Task.Run(_extensions.LoadFeaturesAsync).Result;
            foreach (var feature in allFeatures) {
                var serviceImplTypes = feature.ExportedTypes.Where(t => this.IsRemoteService(t));
                foreach (var serviceImplType in serviceImplTypes) {
                    var serviceMetadata = this.CreateServiceMetadata(feature.FeatureInfo, serviceImplType);
                    descriptors.Add(serviceMetadata);
                }
            }

            //TODO 
            await Task.CompletedTask;
            return descriptors;
        }

        private bool IsRemoteService(Type t) =>
            t.IsPublic && !t.IsGenericType && (t.IsClass || t.IsInterface)
            && t.GetCustomAttribute<RemoteServiceAttribute>() != null;

        private bool IsRemoteServiceMethod(MethodInfo method) =>
            method.IsPublic && !method.IsGenericMethod && !method.IsStatic
            && method.GetCustomAttribute<RemoteServiceMethodAttribute>() != null;


        private RemoteServiceInfo CreateServiceMetadata(IFeatureInfo featureInfo, Type serviceType) {

            var methods =
                serviceType.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .Where(m => this.IsRemoteServiceMethod(m))
                    .Select(method => this.CreateServiceMethodMetadata(method))
                    .ToList();

            var serviceAttr = serviceType.GetCustomAttribute<RemoteServiceAttribute>();
            var rs = new RemoteServiceInfo {
                Feature = featureInfo,
                Name = serviceAttr?.Name ?? serviceType.FullName,
                ClrType = serviceType.GetTypeInfo(),
                Methods = methods
            };
            return rs;
        }

        private RemoteServiceMethodInfo CreateServiceMethodMetadata(MethodInfo method) {
            var methodAttr = method.GetCustomAttribute<RemoteServiceMethodAttribute>();
            return new RemoteServiceMethodInfo() {
                Name = methodAttr?.Name ?? method.Name,
                ClrMethodInfo = method,
            };
        }

    }

}
