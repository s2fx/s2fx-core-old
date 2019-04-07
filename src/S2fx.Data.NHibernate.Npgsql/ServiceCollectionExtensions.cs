using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using S2fx.Data.NHibernate;
using S2fx.Data.NHibernate.Npgsql;

namespace Microsoft.Extensions.DependencyInjection {

    public static class OrchardBuilderExtensions {

        public static OrchardCoreBuilder AddS2fxNHibernateNpgsql(this OrchardCoreBuilder builder) {
            var services = builder.ApplicationServices;
            return builder;

        }
    }

}
