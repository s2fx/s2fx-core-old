using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using S2fx;
using S2fx.Mvc;
using S2fx.Data.NHibernate;
using S2fx.Mvc;

namespace Microsoft.Extensions.DependencyInjection {

    public static class ServiceExtensions {
        /// <summary>
        /// Adds Orchard CMS services to the application. 
        /// </summary>
        public static IServiceCollection AddS2fx(this IServiceCollection services, IConfiguration config) {

            services.AddMvc();
            // Add ASP.NET MVC and support for modules
            var builder = services
                .AddOrchardCore()
                .AddMvc()
                .AddS2Framework()
                .AddS2fxNHibernate()
                .AddS2fxNHNpgsql()
                .AddS2fxNHMSSQLServer()
                .AddS2fxNHSQLite()
                .AddS2fxMvc()
                .AddGlobalFeatures("S2fx.AdminUI")
                .AddTenantFeatures("S2fx.Core")
                //.WithTenants();
                ;

            return services;
        }

    }

}

