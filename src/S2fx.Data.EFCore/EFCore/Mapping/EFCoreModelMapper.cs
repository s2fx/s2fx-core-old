using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using S2fx.Model;
using S2fx.Model.Metadata;

namespace S2fx.Data.EFCore.Mapping {

    public class EFCoreModelMapper : IEFCoreModelMapper {
        private readonly IEFCoreMappingStrategy _strategy;
        private readonly IEntityManager _entityManager;

        public EFCoreModelMapper(IEFCoreMappingStrategy strategy, IEntityManager entityManager) {
            _strategy = strategy;
            _entityManager = entityManager;
        }

        public void RegisterAllEntities(ModelBuilder modelBuilder) {
            var entities = _entityManager.GetEnabledEntities();
            foreach (var entityInfo in entities) {
                _strategy.Build(modelBuilder, entityInfo);
            }
        }
    }

}
