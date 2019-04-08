using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using S2fx.Remoting;
using S2fx;
using S2fx.Environment.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using S2fx.Data.NHibernate.MSSQLServer;

namespace S2fx.Data.NHibernate {

    public static class OrchardBuilderExtensions {

        public static OrchardCoreBuilder AddS2fxNHMSSQLServer(this OrchardCoreBuilder builder, IConfiguration configuration = null) {
            var services = builder.ApplicationServices;
            services.AddTransient<IHibernateDbProvider, MSSQLServerHibernateDbProvider>();
            return builder;
        }

    }

}
