using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Remoting;
using S2fx.Web.Remoting;

namespace S2fx.Web {

    public static class ApplicationBuilderExtensions {

        public static IApplicationBuilder UseS2fxWeb(this IApplicationBuilder app, IServiceProvider serviceProvider, Action<IApplicationBuilder> configure = null) {
            var appPartManager = app.ApplicationServices.GetRequiredService<ApplicationPartManager>();
            appPartManager.FeatureProviders.Add(new RemoteServiceControllerFeatureProvider(serviceProvider.GetRequiredService<IRemoteServiceManager>()));

            // Notify change
            DummyActionDescriptorChangeProvider.Instance.HasChanged = true;
            DummyActionDescriptorChangeProvider.Instance.TokenSource.Cancel();

            return app;
        }

    }
}
