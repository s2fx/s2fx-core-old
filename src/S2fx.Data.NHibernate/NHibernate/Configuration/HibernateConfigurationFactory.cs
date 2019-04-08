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

namespace S2fx.Data.NHibernate {

    public interface IHibernateConfigurationFactory {

        Configuration Create();

    }

    public class HibernateConfigurationFactory : IHibernateConfigurationFactory {
        readonly IModelMapper _mapper;
        readonly S2Settings _settings;
        IHibernateDbProviderAccessor _providerAccessor;
        public ILogger Logger { get; }

        public HibernateConfigurationFactory(
            ILogger<HibernateConfigurationFactory> logger,
            S2Settings settings,
            IModelMapper mapper,
            IHibernateDbProviderAccessor providerAccessor) {
            this.Logger = logger;
            _settings = settings;
            _mapper = mapper;
            _providerAccessor = providerAccessor;
        }

        public Configuration Create() {
            var cfg = new Configuration();
            var provider = _providerAccessor.EnabledDbProvider;
            provider.SetupConfiguration(cfg);
            cfg.SetConnectionString(_settings.Db.DefaultConnectionString);
            cfg.SetProperty("hbm2ddl.keywords", "auto-quote");
            _mapper.MapAllEntities(cfg);
            return cfg;
        }

    }

}
