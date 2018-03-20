using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;
using S2fx.Data.NHibernate.Mapping;

namespace S2fx.Data.NHibernate {

    public interface INhConfigurationFactory {

        Configuration Create();

    }

    public class NhConfigurationFactory : INhConfigurationFactory {
        private readonly INhModelMapper _mapper;

        public NhConfigurationFactory(INhModelMapper mapper) {
            _mapper = mapper;
        }

        public Configuration Create() {
            var cfg = new Configuration();
            cfg.UseNpgsql();
            cfg.SetConnectionString("Host=localhost;Database=s2fxdb;Username=s2fx;Password=s2fx");
            _mapper.MapAllEntities(cfg);
            return cfg;
        }

    }

}
