using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using S2fx.Remoting;
using S2fx.Web.Remoting;
using S2fx;
using S2fx.Environment.Configuration;
using S2fx.Web.Environment.Configuration;
using Microsoft.Extensions.Configuration;

namespace S2fx.Web {

    public static class OrchardBuilderExtensions {

        public static OrchardCoreBuilder AddS2fxWeb(this OrchardCoreBuilder builder, IConfiguration configuration) {
            var services = builder.ApplicationServices;

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
            services.AddSingleton(LoadSettings(configuration));

            services.AddSingleton<IApplicationFeatureProvider<ControllerFeature>, RemoteServiceControllerFeatureProvider>();

            services.AddSingleton<IActionDescriptorChangeProvider>(DummyActionDescriptorChangeProvider.Instance);
            services.AddSingleton(DummyActionDescriptorChangeProvider.Instance);
            return builder;
        }

        private static S2Settings LoadSettings(IConfiguration configuration) {
            var loader = new MvcConfigurationLoader(configuration);
            return loader.GetSettings();
        }
    }

}
