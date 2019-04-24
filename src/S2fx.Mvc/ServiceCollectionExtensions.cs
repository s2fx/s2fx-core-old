using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using S2fx.Environment.Configuration;
using S2fx.Mvc.Environment.Configuration;
using S2fx.Mvc.Remoting;
using S2fx.Remoting;

namespace S2fx.Mvc {

    public static class ServiceCollectionExtensions {

        /*
        public static void AddS2MvcTenant(this IServiceCollection services) {
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
            //var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            services.AddSingleton(LoadSettings(configuration));

            services.AddSingleton<IApplicationFeatureProvider<ControllerFeature>, RemoteServiceControllerFeatureProvider>();

            services.AddSingleton<IActionDescriptorChangeProvider>(DummyActionDescriptorChangeProvider.Instance);
            services.AddSingleton(DummyActionDescriptorChangeProvider.Instance);

            // Notify change
            //DummyActionDescriptorChangeProvider.Instance.HasChanged = true;
            //DummyActionDescriptorChangeProvider.Instance.TokenSource.Cancel();
            // Register an isolated tenant part manager.
            var appPartManager = mvcBuilder.PartManager;
            var httpContextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var controllerFeatureProvider = new RemoteServiceControllerFeatureProvider(httpContextAccessor);
            appPartManager.FeatureProviders.Add(controllerFeatureProvider);
        }

        private static S2Settings LoadSettings(IServiceProvider sp) {
            var cfg = sp.GetRequiredService<IConfiguration>();
            var loader = new MvcConfigurationLoader(cfg);
            return loader.GetSettings();
        }
        */

    }

}
