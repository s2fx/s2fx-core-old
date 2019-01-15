using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Remoting;
using S2fx.Web.Remoting;

namespace S2fx.Web {

    public static class ApplicationBuilderExtensions {

        public static IApplicationBuilder UseS2fxWeb(this IApplicationBuilder app, Action<IApplicationBuilder> configure = null) {
            var appPartManager = app.ApplicationServices.GetRequiredService<ApplicationPartManager>();

            foreach (var controllerFeatureProvider in app.ApplicationServices.GetServices<IApplicationFeatureProvider<ControllerFeature>>()) {
                appPartManager.FeatureProviders.Add(controllerFeatureProvider);
            }

            // Notify change
            //DummyActionDescriptorChangeProvider.Instance.HasChanged = true;
            //DummyActionDescriptorChangeProvider.Instance.TokenSource.Cancel();
            configure?.Invoke(app);

            return app;
        }

    }
}
