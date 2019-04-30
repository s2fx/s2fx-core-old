using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Modules.Services;

namespace S2fx.Modules {
    internal static class ServiceCollectionExtensions {

        internal static void AddS2ModuleTenantServices(this IServiceCollection services) {
            services.AddTransient<IS2StartupService, S2StartupService>();
        }
    }
}
