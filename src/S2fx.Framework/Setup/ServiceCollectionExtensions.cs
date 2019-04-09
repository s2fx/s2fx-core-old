using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using S2fx.Data.Seeding;
using S2fx.Data.UnitOfWork;
using S2fx.Remoting;
using S2fx.Remoting.RemoteServices;
using S2fx.Remoting.RemoteServices.Metadata;
using S2fx.Setup.RemoteService;
using S2fx.Setup.Services;

namespace S2fx.Setup {

    public static class ServiceCollectionExtensions {

        public static void AddSetupServices(this IServiceCollection services) {
            services.AddScoped<ISetupService, SetupService>();

            services.AddInternalRemoteServices();
        }

        public static void AddInternalRemoteServices(this IServiceCollection services) {
            //builtin remote services
            services.AddScoped<SetupRemoteService>();
        }
    }

}
