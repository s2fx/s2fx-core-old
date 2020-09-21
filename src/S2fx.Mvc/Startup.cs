using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
using System.Linq;
using S2fx.Mvc.Data.Transactions;

namespace S2fx.Mvc {

    public class Startup : StartupBase {

        public override int Order => -100;
        public override int ConfigureOrder => 100;

        private readonly IServiceProvider _serviceProvider;

        public Startup(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public override void ConfigureServices(IServiceCollection services) {

            this.RegisterRemoteServicesAsControllers(services);

            //Add settings to Service Collection
            var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            services.AddSingleton(LoadSettings(configuration));

            services.AddSingleton<IApplicationFeatureProvider<ControllerFeature>, RemoteServiceControllerFeatureProvider>();

            services.AddSingleton<IActionDescriptorChangeProvider>(DummyActionDescriptorChangeProvider.Instance);
            services.AddSingleton(DummyActionDescriptorChangeProvider.Instance);

            // Notify change
            //DummyActionDescriptorChangeProvider.Instance.HasChanged = true;
            //DummyActionDescriptorChangeProvider.Instance.TokenSource.Cancel();
            // Register an isolated tenant part manager.
        }

        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider) {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMiddleware<TransactionalMiddleware>();
        }

        private void RegisterRemoteServicesAsControllers(IServiceCollection services) {
            var mvcBuilder = services.AddMvcCore();
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
                services.AddTransient<IModelBinderProvider, EntityQueryParametersModelBinderProvider>();

                services.AddTransient<IConfigureOptions<MvcOptions>, RemoteServiceMvcConfigureOptions>();
                //TODO FIXME
                //services.AddTransient<IConfigureOptions<MvcJsonOptions>, RemoteServiceMvcJsonConfigureOptions>();
                services.AddTransient<IRemoteServiceProvider, MvcControllerRemoteServiceProvider>();
            }


            var appPartManager = mvcBuilder.PartManager;
            var httpContextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var controllerFeatureProvider = new RemoteServiceControllerFeatureProvider(httpContextAccessor);
            appPartManager.FeatureProviders.Add(controllerFeatureProvider);
        }

        private static S2AppSettings LoadSettings(IConfiguration configuration) {
            var loader = new MvcConfigurationLoader(configuration);
            return loader.GetSettings();
        }

    }
}
