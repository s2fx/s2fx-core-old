using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OrchardCore.Modules;
using S2fx.Remoting.RemoteServices;
using S2fx.Remoting.RemoteServices.Metadata;

namespace S2fx.Remoting {

    public static class ServiceCollectionExtensions {

        public static void AddRemotingTenants(this IServiceCollection services) {
            services.AddSingleton<IRemoteServiceManager, RemoteServiceManager>();
            services.AddScoped<IModularTenantEvents, RemotingInitializer>();

            //metadata providers:
            services.TryAddEnumerable(new[] {
                ServiceDescriptor.Transient<IRemoteServiceMetadataProvider, BuiltinRemoteServiceMetadataProvider>(),
                ServiceDescriptor.Transient<IRemoteServiceMetadataProvider, GenericEntityRemoteServiceMetadataProvider>(),
                ServiceDescriptor.Transient<IRemoteServiceMetadataProvider, CustomRemoteServiceMetadataProvider>(),
            });
        }

        public static void AddInternalRemoteServicesTenant(this IServiceCollection services) {

            //builtin remote services
            services.AddScoped(typeof(GenericRestEntityRemoteService<>));
        }
    }

}
