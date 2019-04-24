using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace S2fx.Xaml {

    internal static class ServiceCollectionExtensions {

        internal static void AddXamlSupportGlobal(this IServiceCollection services) {
            services.AddTransient<IXamlService, PortableXamlXamlService>();
        }

    }

}
