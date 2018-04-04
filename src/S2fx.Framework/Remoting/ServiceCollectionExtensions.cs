using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Convention;
using S2fx.Data.Seeding;
using S2fx.Data.UnitOfWork;
using S2fx.Remoting;
using S2fx.Remoting.RemoteServices.Metadata;

namespace S2fx.Remoting {

    public static class ServiceCollectionExtensions {

        public static void AddRemoting(this IServiceCollection services) {
            services.AddSingleton<IRemoteServiceManager, RemoteServiceManager>();

            //metadata providers:
            services.AddTransient<IRemoteServiceMetadataProvider, BuiltinRemoteServiceMetadataProvider>();
            services.AddTransient<IRemoteServiceMetadataProvider, ShellRemoteServiceMetadataProvider>();
        }

        public static void AddBuiltinRemoteServices(this IServiceCollection services) {

            //builtin remote services
            services.AddScoped<MetaEntityRemoteService>();
        }
    }

}
