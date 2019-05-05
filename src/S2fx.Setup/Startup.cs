using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using S2fx.Data.Importing;
using S2fx.Modules;
using S2fx.View;

namespace S2fx.Setup {

    /// <summary>
    /// The module startup
    /// </summary>
    public class Startup : StartupBase {

        public override void ConfigureServices(IServiceCollection services) {
            services.AddSetupServices();
        }

        public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaRoute(
                name: "Setup",
                areaName: "S2fx.Setup",
                template: "",
                defaults: new { controller = "Setup", action = "Index" }
            );
        }

    }
}