using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Web.Remoting;

namespace S2fx.Web {

    public static class ServiceCollectionExtensions {

        public static void AddSlipStreamFrameworkWeb(this IServiceCollection services) {
            services.AddSingleton<IActionDescriptorChangeProvider>(DummyActionDescriptorChangeProvider.Instance);
            services.AddSingleton(DummyActionDescriptorChangeProvider.Instance);
        }

    }

}
