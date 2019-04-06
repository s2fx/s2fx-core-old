using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using OrchardCore.Environment.Shell;
using System.IO;
using Microsoft.Extensions.Options;

namespace S2fx.AdminUI {
    public class Startup : StartupBase {
        public const string NgClientUrlPrefix = "ng-client";

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) {
            _configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services) {

        }

        public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider) {
            routes.MapAreaRoute(
                name: "Admin",
                areaName: "S2fx.AdminUI",
                template: "Admin",
                defaults: new { controller = "Admin", action = "Index" }
            );

            //routes.MapRoute("default", "{controller}/{action}");
            //routes.MapRoute("Admin", "{*url}", defaults: new { controller = "Admin", action = "Index" });


            //routes.MapRoute("default", "{controller}/{action=Index}");
            //routes.MapRoute("spa", "{*url}"); // This should serve SPA index.html

        }
    }
}