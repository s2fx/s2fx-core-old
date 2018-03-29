using System;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Remoting;
using S2fx.Remoting.Conventions;
using WampSharp.V2;

namespace S2fx.WampSharp {

    public static class ServiceCollectionExtensions {

        public static void AddS2Wamp(this IServiceCollection services) {

            services.AddS2DefaultWampConventions();

            services.AddTransient<ICalleeRegistrationInterceptor, S2CalleeRegistrationInterceptor>();

        }

        public static void AddS2DefaultWampConventions(this IServiceCollection services) {
            services.AddTransient<IWampHostConvention, DefaultWampHostConvention>();
            services.AddTransient<IWampRealmConvention, DefaultWampRealmConvention>();
        }

    }

}
