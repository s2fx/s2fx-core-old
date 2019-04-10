using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Remoting;
using S2fx.Mvc.Remoting;

namespace S2fx.Mvc {

    public static class ApplicationBuilderExtensions {

        public static IApplicationBuilder UseS2fxMvc(this IApplicationBuilder app) {
            var appPartManager = app.ApplicationServices.GetRequiredService<ApplicationPartManager>();

            foreach (var controllerFeatureProvider in app.ApplicationServices.GetServices<IApplicationFeatureProvider<ControllerFeature>>()) {
                appPartManager.FeatureProviders.Add(controllerFeatureProvider);
            }

            // Notify change
            //DummyActionDescriptorChangeProvider.Instance.HasChanged = true;
            //DummyActionDescriptorChangeProvider.Instance.TokenSource.Cancel();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            return app;
        }

    }
}
