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

    public abstract class AbstractClrTypeBasedRemoteServiceMetadataProvider : IRemoteServiceMetadataProvider {

        public abstract Task<IEnumerable<RemoteServiceInfo>> GetAllServicesAsync();

        protected bool IsRemoteService(Type t) =>
            t.IsPublic && !t.IsGenericType && t.IsClass
            && t.GetCustomAttribute<RemoteServiceAttribute>() != null;

        protected bool IsRemoteServiceMethod(MethodInfo method) =>
            method.IsPublic && !method.IsGenericMethod && !method.IsStatic
            && method.GetCustomAttribute<RemoteServiceMethodAttribute>() != null;


        protected RemoteServiceInfo CreateServiceMetadata(IFeatureInfo featureInfo, Type serviceType) {
            var serviceAttr = serviceType.GetCustomAttribute<RemoteServiceAttribute>();
            var name = string.IsNullOrEmpty(serviceAttr.Name) ? serviceType.FullName : serviceAttr.Name;
            var area = string.IsNullOrEmpty(serviceAttr.Area) ? featureInfo.Id : serviceAttr.Area;
            var rs = this.CreateServiceMetadata(name, featureInfo, serviceType.GetTypeInfo(), area);
            return rs;
        }

        protected RemoteServiceInfo CreateServiceMetadata(string name, IFeatureInfo featureInfo, Type serviceType, string area) {

            var methods =
                serviceType.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .Where(m => this.IsRemoteServiceMethod(m))
                    .Select(method => this.CreateServiceMethodMetadata(method))
                    .ToList();

            var rs = new RemoteServiceInfo {
                Feature = featureInfo,
                Name = name,
                ClrType = serviceType.GetTypeInfo(),
                Methods = methods,
                Area = area,
            };
            return rs;
        }

        protected RemoteServiceMethodInfo CreateServiceMethodMetadata(MethodInfo method) {
            var methodAttr = method.GetCustomAttribute<RemoteServiceMethodAttribute>();
            var name = string.IsNullOrEmpty(methodAttr.Name) ? method.Name : methodAttr.Name;
            return new RemoteServiceMethodInfo() {
                Name = name,
                ClrMethodInfo = method,
            };
        }

    }

}
