using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using S2fx.Model.Entities;
using S2fx.Model.Metadata;
using S2fx.Utility;
using S2fx.Data.Convention;
using Microsoft.EntityFrameworkCore.Metadata;
using S2fx.Model.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S2fx.Model;

namespace S2fx.Data.EFCore.Mapping {

    public class DefaultEFCoreMappingStrategy : IEFCoreMappingStrategy {
        private readonly IDbNameConvention _dbNameConvention;
        private readonly IEntityManager _entities;

        public DefaultEFCoreMappingStrategy(IDbNameConvention nameConvention, IEntityManager entities) {
            _dbNameConvention = nameConvention;
            _entities = entities;
        }

        public void MapEntity(ModelBuilder modelBuilder, EntityInfo entityInfo) {


            //Register entity type
            var entity = modelBuilder.Entity(entityInfo.ClrType)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            //Set the `Id` property as primary key
            entity.HasKey(nameof(IEntity.Id));

            foreach (var pi in entityInfo.Properties) {
                this.DoPropertyMapping(entity, entityInfo, pi.Value);
            }

            //Process name convention.
            var efEntityType = modelBuilder.Model.GetEntityTypes().Single(x => x.ClrType == entityInfo.ClrType);
            efEntityType.Relational().TableName = _dbNameConvention.EntityToTable(entityInfo.Name);

            foreach (var p in efEntityType.GetProperties()) {
                p.Relational().ColumnName = _dbNameConvention.EntityPropertyToColumn(p.Name);
            }
        }

        private void DoPropertyMapping(EntityTypeBuilder entityBuilder, EntityInfo entityInfo, EntityPropertyInfo prop) {

            switch (prop.PropertyType) {

                case EntityPropertyType.Primitive:
                    entityBuilder.Property(prop.ClrPropertyInfo.PropertyType, prop.Name);
                    break;

                case EntityPropertyType.ManyToOne:
                    var relationColumnName = _dbNameConvention.EntityPropertyToColumn(prop.Name);
                    entityBuilder
                        .HasOne(prop.ClrPropertyInfo.PropertyType, prop.Name)
                        .WithMany()
                        .HasForeignKey(relationColumnName);
                    break;

                case EntityPropertyType.OneToMany:
                    //FIXME 
                    var o2mAttr = prop.GetPropertyAttribute<OneToManyPropertyAttribute>();
                    var refEntity = _entities.GetEntity(o2mAttr.RefEntity);
                    entityBuilder
                       .HasMany(refEntity.ClrType, prop.Name)
                       .WithOne();
                    break;

                default:
                    throw new NotSupportedException();
            }

        }

    }

}
