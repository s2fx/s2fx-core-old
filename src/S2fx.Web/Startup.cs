using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using S2fx.Environment.Configuration;
using S2fx.Environment.Extensions.Remoting;
using S2fx.Remoting;
using S2fx.Web.Rpc.Metadata;
using S2fx.Web.Controllers;
using S2fx.Web.Environment.Configuration;
using S2fx.Web.Remoting;

namespace S2fx.Web {
    public class Startup : StartupBase {

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) {
            _configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services) {

            /*
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<IDisplayDriver<ISite>, SmtpSettingsDisplayDriver>();
            services.AddScoped<INavigationProvider, AdminMenu>();

            services.AddScoped<IConfigureOptions<SmtpSettings>, SmtpSettingsConfiguration>();
            services.AddScoped<ISmtpService, SmtpService>();
            */

            //Remote services
            {
                services.AddTransient<RemoteServiceControllerNameConvention>();
                services.AddTransient<IConfigureOptions<MvcOptions>, RemoteServiceMvcConfigureOptions>();
                services.AddTransient<IRemoteServiceProvider, MvcControllerRemoteServiceProvider>();

                //builtin remote services
                services.AddScoped<MetaEntityRemoteService>();
            }

            //Add settings to Service Collection
            services.AddSingleton(this.LoadSettings());

        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider) {

            routes.MapAreaRoute(
                name: "Home",
                areaName: "S2fx.Web",
                template: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            app.UseStaticFiles();

            var appPartManager = app.ApplicationServices.GetRequiredService<ApplicationPartManager>();
            appPartManager.FeatureProviders.Add(new RemoteServiceControllerFeatureProvider(serviceProvider));
        }

        private S2Settings LoadSettings() {
            var loader = new MvcConfigurationLoader(this._configuration);
            return loader.GetSettings();
        }

    }
}