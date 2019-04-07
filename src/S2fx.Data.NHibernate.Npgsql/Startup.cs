using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;

namespace S2fx.Data.NHibernate.Npgsql {

    public class Startup : StartupBase {

        public override void ConfigureServices(IServiceCollection services) {
            services.WithNHibernate();

            services.AddTransient<IHibernateDbProvider, PostgreSQLHibernateDbProvider>();
        }
    }
}