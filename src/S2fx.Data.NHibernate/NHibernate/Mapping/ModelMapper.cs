using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using S2fx.Model;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;

namespace S2fx.Data.NHibernate.Mapping {

    public interface IModelMapper {
        void MapAllEntities(Configuration cfg);
    }

    public class ModelMapper : IModelMapper {
        private readonly IServiceProvider _services;
        private readonly IEntityManager _entityManager;

        public ModelMapper(IServiceProvider services, IEntityManager entityManager) {
            _services = services;
            _entityManager = entityManager;
        }

        public void MapAllEntities(Configuration cfg) {
            var mapper = new global::NHibernate.Mapping.ByCode.ModelMapper();

            //Only maps SQL table based entities
            var entities = _entityManager.GetEntities().Values
                .Where(x => x.EntityType.Name == BuiltinEntityTypeNames.SqlEntityTypeName);

            foreach (var entityInfo in entities) {
                var entityMapping = this.GetEntityClassMapping(entityInfo);
                mapper.AddMapping(entityMapping);
            }

            var hbm = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddDeserializedMapping(hbm, WellKnownConstants.MappingDocumentName);
        }

        private IConformistHoldersProvider GetEntityClassMapping(MetaEntity entityInfo) {
            var classMappingType = typeof(EntityMappingClass<>).MakeGenericType(entityInfo.ClrType);
            var classMapping = _services.GetService(classMappingType) as IConformistHoldersProvider;
            return classMapping;
        }

    }

}
