using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using S2fx.Model;
using S2fx.Model.Metadata;

namespace S2fx.Data.NHibernate.Mapping {

    public interface INhModelMapper {
        void MapAllEntities(Configuration cfg);
    }

    public class NhModelMapper : INhModelMapper {
        private readonly IServiceProvider _services;
        private readonly IEntityManager _entityManager;

        public NhModelMapper(IServiceProvider services, IEntityManager entityManager) {
            _services = services;
            _entityManager = entityManager;
        }

        public void MapAllEntities(Configuration cfg) {
            var mapper = new ModelMapper();

            var entities = _entityManager.GetEnabledEntities();
            foreach (var entityInfo in entities) {
                var entityMapping = this.GetEntityClassMapping(entityInfo);
                mapper.AddMapping(entityMapping);
            }

            var hbm = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddDeserializedMapping(hbm, WellKnownConstants.MappingDocumentName);
        }

        private IConformistHoldersProvider GetEntityClassMapping(MetaEntity entityInfo) {
            var classMappingType = typeof(NhEntityMappingClass<>).MakeGenericType(entityInfo.ClrType);
            var classMapping = _services.GetService(classMappingType) as IConformistHoldersProvider;
            return classMapping;
        }

    }

}
