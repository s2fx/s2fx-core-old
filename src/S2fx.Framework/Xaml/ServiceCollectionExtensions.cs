using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Data.Seeding;
using S2fx.Data.UnitOfWork;
using S2fx.Remoting;

namespace S2fx.Xaml {

    public static class ServiceCollectionExtensions {

        public static void AddXamlSupport(this IServiceCollection services) {
            services.AddTransient<IXamlService, PortableXamlXamlService>();
        }

    }

}
