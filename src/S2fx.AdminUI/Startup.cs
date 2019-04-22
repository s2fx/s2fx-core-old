using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.Environment.Shell;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using S2fx.AdminUI.StaticFiles;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace S2fx.AdminUI {
    public class Startup : StartupBase {
        public const string NgClientUrlPrefix = "ng-client";

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) {
            _configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services) {

            services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<AdminUIOptions>,
                AdminUIOptionsSetup>());

            services.AddSingleton<ISpaStaticFileProvider>(s => new AdminUISpaStaticFileProvider(s));

        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider) {

            app.Map("/admin", ab => {
                ab.UseSpaStaticFiles();
                ab.UseSpa(spa => {
                    // To learn more about options for serving an Angular SPA from ASP.NET Core,
                    // see https://go.microsoft.com/fwlink/?linkid=864501

                    spa.Options.SourcePath = "Client";
                    spa.Options.DefaultPage = $"/admin/{AdminUIConstants.DefaultPageName}";
                    var env = serviceProvider.GetService<IHostingEnvironment>();
                    if (env.IsDevelopment()) {
                        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                        //spa.UseAngularCliServer(npmScript: "start");
                    }
                });
            });

        }

    }
}