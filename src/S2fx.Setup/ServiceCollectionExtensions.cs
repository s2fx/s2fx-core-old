using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using S2fx.Setup.RemoteService;
using S2fx.Setup.Services;

namespace S2fx.Setup {

    internal static class ServiceCollectionExtensions {

        internal static void AddSetupServices(this IServiceCollection services) {
            services.AddScoped<ISetupService, SetupService>();
        }
    }

}
