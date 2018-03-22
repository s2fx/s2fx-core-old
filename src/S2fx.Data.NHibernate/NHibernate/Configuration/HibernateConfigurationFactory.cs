using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;
using S2fx.Data.NHibernate.Mapping;
using S2fx.Environment.Configuration;

namespace S2fx.Data.NHibernate {

    public interface IHibernateConfigurationFactory {

        Configuration Create();

    }

    public class HibernateConfigurationFactory : IHibernateConfigurationFactory {
        private readonly IModelMapper _mapper;
        private readonly S2Settings _settings;

        public HibernateConfigurationFactory(
            S2Settings settings, IModelMapper mapper) {
            _settings = settings;
            _mapper = mapper;
        }

        public Configuration Create() {
            var cfg = new Configuration();
            cfg.UseNpgsql();
            cfg.SetConnectionString(_settings.Db.DefaultConnectionString);
            cfg.SetProperty("hbm2ddl.keywords", "auto-quote");
            _mapper.MapAllEntities(cfg);
            return cfg;
        }

    }

}
