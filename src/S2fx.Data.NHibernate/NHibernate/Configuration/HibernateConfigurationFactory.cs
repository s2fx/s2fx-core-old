using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;
using S2fx.Data.NHibernate.Mapping;
using S2fx.Environment.Configuration;
using S2fx.Data;
using System.Linq;
using Microsoft.Extensions.Logging;
using S2fx.Data.NHibernate.DbProviders;
using NHibernate;

namespace S2fx.Data.NHibernate {

    public interface IHibernateConfigurationFactory {

        Configuration Create();

    }

    public class HibernateConfigurationFactory : IHibernateConfigurationFactory {
        readonly IModelMapper _mapper;
        readonly S2Settings _settings;
        readonly IInterceptor _interceptor;
        IHibernateDbProviderAccessor _providerAccessor;
        public ILogger Logger { get; }

        public HibernateConfigurationFactory(
            ILogger<HibernateConfigurationFactory> logger,
            S2Settings settings,
            IModelMapper mapper,
            IHibernateDbProviderAccessor providerAccessor,
            IInterceptor interceptor) {
            this.Logger = logger;
            _settings = settings;
            _mapper = mapper;
            _providerAccessor = providerAccessor;
            _interceptor = interceptor;
        }

        public Configuration Create() {
            var cfg = new Configuration();

            var provider = _providerAccessor.EnabledDbProvider;

            provider.SetupConfiguration(cfg);

            cfg.SetInterceptor(_interceptor);

            cfg.SetConnectionString(_settings.Db.DefaultConnectionString);
            cfg.SetProperty("hbm2ddl.keywords", "auto-quote");
            this.Logger.LogDebug("Loading NHibernate mapping...");
            _mapper.MapAllEntities(cfg);
            this.Logger.LogDebug("NHibernate mapping loaded.");
            return cfg;
        }

    }

}
