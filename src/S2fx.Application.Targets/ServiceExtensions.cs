using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using S2fx;
using S2fx.Mvc;
using S2fx.Data.NHibernate;
using OrchardCore.Environment.Shell.Configuration;
using Microsoft.Extensions.Options;
using OrchardCore.Environment.Shell;
using S2fx.Environment.Shell.Descriptor;
using OrchardCore.Environment.Shell.Descriptor;

namespace Microsoft.Extensions.DependencyInjection {

    public static class ServiceExtensions {
        /// <summary>
        /// Adds Orchard CMS services to the application. 
        /// </summary>
        public static IServiceCollection AddS2fx(this IServiceCollection services, Action<OrchardCoreBuilder> config = null) {

            // Add ASP.NET MVC and support for modules
            var builder = services
                .AddOrchardCore()
                .AddS2Framework()
                .AddS2fxNHibernate()
                .AddS2fxNHNpgsql()
                .AddS2fxNHMSSQLServer()
                .AddS2fxNHSQLite()
                .AddS2fxMvc()
                //.AddGlobalFeatures("S2fx.Mvc")
                .AddGlobalFeatures("S2fx.AdminUI")
                .AddTenantFeatures("S2fx.Core")
                .AddSetupFeatures("S2fx.Setup")
                ;

            if (config != null) {
                config(builder);
            }

            return services;
        }


    }

}

