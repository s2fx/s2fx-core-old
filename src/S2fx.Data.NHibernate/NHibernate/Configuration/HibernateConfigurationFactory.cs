using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;
using S2fx.Data.NHibernate.Mapping;
using S2fx.Environment.Configuration;
using S2fx.Data;
using System.Linq;

namespace S2fx.Data.NHibernate {

    public interface IHibernateConfigurationFactory {

        Configuration Create();

    }

    public class HibernateConfigurationFactory : IHibernateConfigurationFactory {
        private readonly IModelMapper _mapper;
        private readonly S2Settings _settings;
        private readonly IEnumerable<IHibernateDbProvider> _providers;

        public HibernateConfigurationFactory(
            S2Settings settings,
            IModelMapper mapper,
            IEnumerable<IHibernateDbProvider> providers) {
            _settings = settings;
            _mapper = mapper;
            _providers = providers;
        }

        public Configuration Create() {
            var cfg = new Configuration();

            var provider = _providers.SingleOrDefault(x => x.Name == _settings.Db.Provider);
            if (provider == null) {
                throw new NotSupportedException($"Not supported database provider: '{_settings.Db.Provider}'");
            }
            provider.SetupConfiguration(cfg);
            cfg.SetConnectionString(_settings.Db.DefaultConnectionString);
            cfg.SetProperty("hbm2ddl.keywords", "auto-quote");
            _mapper.MapAllEntities(cfg);
            return cfg;
        }

    }

}
