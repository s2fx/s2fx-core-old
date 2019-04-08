using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using S2fx.Data.Seeding;
using S2fx.Data.UnitOfWork;
using S2fx.Remoting;
using S2fx.Remoting.RemoteServices;
using S2fx.Remoting.RemoteServices.Metadata;

namespace S2fx.Services {

    public static class ServiceCollectionExtensions {

        public static void AddS2Services(this IServiceCollection services) {
            services.AddTransient<IClock, Clock>();
        }

    }

}
