using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NHibernate;
using NHibernate.Cfg;
using OrchardCore.Modules;
using S2fx.Data.NHibernate.Mapping;
using S2fx.Data.NHibernate.Mapping.Properties;
using S2fx.Data.NHibernate.UnitOfWork;
using S2fx.Data.UnitOfWork;
using S2fx.Utility;

namespace S2fx.Data.NHibernate {

    public class Startup : StartupBase {

        public override void ConfigureServices(IServiceCollection services) {
            services.AddS2fxNHibernate();
        }

    }
}