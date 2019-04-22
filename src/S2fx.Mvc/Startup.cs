using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using S2fx.Environment.Configuration;
using S2fx.Mvc.Environment.Configuration;
using S2fx.Mvc.Remoting;
using S2fx.Remoting;

namespace S2fx.Mvc {

    public class Startup : StartupBase {

        public override int Order => -200;

        private readonly IServiceProvider _serviceProvider;

        public Startup(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider) {
         
            // Notify change
            //DummyActionDescriptorChangeProvider.Instance.HasChanged = true;
            //DummyActionDescriptorChangeProvider.Instance.TokenSource.Cancel();

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }

        public override void ConfigureServices(IServiceCollection services) {

            // Register an isolated tenant part manager.
            var appPartManager = _serviceProvider.GetRequiredService<ApplicationPartManager>();
            foreach (var controllerFeatureProvider in _serviceProvider.GetServices<IApplicationFeatureProvider<ControllerFeature>>()) {
                appPartManager.FeatureProviders.Add(controllerFeatureProvider);
            }

            //Remote services
            {
                {
                    //Controller Conventions
                    services.AddTransient<IControllerModelConvention, RemoteServiceControllerConvention>();

                    // Controller Action Conventions
                    services.AddTransient<IActionModelConvention, RemoteServiceMethodActionModelConvention>();

                    // Controller Parameter Conventions
                    services.AddTransient<IParameterModelConvention, RemoteServiceControllerActionParameterConvention>();
                }

                //Model Binder Providers:
                {
                    services.AddTransient<IModelBinderProvider, EntityQueryParametersModelBinderProvider>();
                }

                services.AddTransient<IConfigureOptions<MvcOptions>, RemoteServiceMvcConfigureOptions>();
                services.AddTransient<IConfigureOptions<MvcJsonOptions>, RemoteServiceMvcJsonConfigureOptions>();
                services.AddTransient<IRemoteServiceProvider, MvcControllerRemoteServiceProvider>();
            }

            //Add settings to Service Collection
            var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            services.AddSingleton(LoadSettings(configuration));

            services.AddSingleton<IApplicationFeatureProvider<ControllerFeature>, RemoteServiceControllerFeatureProvider>();

            services.AddSingleton<IActionDescriptorChangeProvider>(DummyActionDescriptorChangeProvider.Instance);
            services.AddSingleton(DummyActionDescriptorChangeProvider.Instance);
        }

        private static S2Settings LoadSettings(IConfiguration configuration) {
            var loader = new MvcConfigurationLoader(configuration);
            return loader.GetSettings();
        }

    }
}
