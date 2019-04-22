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
using S2fx.Data.NHibernate.Npgsql;
using Npgsql;

namespace S2fx.Data.NHibernate {

    public static class OrchardBuilderExtensions {

        public static OrchardCoreBuilder AddS2fxNHNpgsql(this OrchardCoreBuilder builder, IConfiguration configuration = null) {
            //Enables the  JSON support in Npgsql
            NpgsqlConnection.GlobalTypeMapper.UseJsonNet();
            builder.ApplicationServices.AddTransient<IDbProvider, PostgreSQLHibernateDbProvider>();
            return builder;
        }

    }

}
