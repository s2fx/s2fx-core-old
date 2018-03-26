using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Convention;
using S2fx.Data.Seeding;
using S2fx.Data.UnitOfWork;
using S2fx.Remoting;

namespace S2fx.Remoting {

    public static class ServiceCollectionExtensions {

        public static void AddRemoting(this IServiceCollection services) {
            services.AddSingleton<IRemoteServiceManager, RemoteServiceManager>();
            services.AddTransient<IRemoteServiceMetadataProvider, ModuleAssemblyRemoteServiceMetadataProvider>();
        }

    }

}
