using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using S2fx.Model.Entities;
using S2fx.Model.Metadata;
using S2fx.Utility;
using S2fx.Data.Convention;

namespace S2fx.Data.EFCore.Mapping {

    public class DefaultEFCoreMappingStrategy : IEFCoreMappingStrategy {
        private readonly IDbNameConvention _dbNameConvention;

        public DefaultEFCoreMappingStrategy(IDbNameConvention nameConvention) {
            _dbNameConvention = nameConvention;
        }

        public void Build(ModelBuilder modelBuilder, EntityInfo entityInfo) {


            //Register entity type
            var entity = modelBuilder.Entity(entityInfo.Type);

            //Set the `Id` property as primary key
            entity.HasKey(nameof(IEntity.Id));

            //TODO
            if (entityInfo.Type.IsImplementsInterface<IAuditedEntity>()) {
                entity.Ignore(nameof(IAuditedEntity.CreatedBy));
                entity.Ignore(nameof(IAuditedEntity.UpdatedBy));
            }

            //Process property's name convention.
            var efEntityType = modelBuilder.Model.GetEntityTypes().Single(x => x.ClrType == entityInfo.Type);
            foreach (var p in efEntityType.GetProperties()) {
                p.Relational().ColumnName = _dbNameConvention.EntityPropertyToColumn(p.Name);
            }
        }

    }

}
