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

namespace S2fx.Web {

    public static class ServiceCollectionExtensions {

        public static void S2fxWeb(this IServiceCollection services, OrchardCoreBuilder builder) {

            services.AddS2Framework(builder);

            //Remote services
            {
                //Controller Conventions
                {
                    services.AddTransient<IControllerModelConvention, RemoteServiceControllerNameConvention>();
                    services.AddTransient<IControllerModelConvention, RemoteServiceControllerActionConvention>();
                    services.AddTransient<IControllerModelConvention, RemoteServiceControllerAreaConvention>();
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
            //services.AddSingleton(this.LoadSettings());

            services.AddSingleton<IApplicationFeatureProvider<ControllerFeature>, RemoteServiceControllerFeatureProvider>();

            services.AddSingleton<IActionDescriptorChangeProvider>(DummyActionDescriptorChangeProvider.Instance);
            services.AddSingleton(DummyActionDescriptorChangeProvider.Instance);
        }

        /*
        private static S2Settings LoadSettings() {
            var loader = new MvcConfigurationLoader(this._configuration);
            return loader.GetSettings();
        }
        */
    }

}
