using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Extensions.Features;
using S2fx.Model.Annotations;
using S2fx.Model.Metadata.Types;

namespace S2fx.Environment.Extensions.Entity {

    public abstract class AbstractClrEntityHarvester : IEntityHarvester {

        public abstract int Priority { get; }
        public abstract Task<IEnumerable<EntityInfo>> HarvestEntitiesAsync();

        protected IEnumerable<EntityInfo> HarvestClrEntityInFeature(FeatureEntry feature) {
            var entityTypes = feature.ExportedTypes.Where(x => this.IsEntityType(x));

            foreach (var et in entityTypes) {
                var entity = GetEntityInfo(feature.FeatureInfo, et);
                yield return entity;
            }
        }

        protected EntityInfo GetEntityInfo(IFeatureInfo feature, Type et) {
            var entityAttr = et.GetCustomAttribute<EntityAttribute>();
            var attributes = et.GetCustomAttributes();
            var entity = new EntityInfo {
                Feature = feature,
                EntityType = BuiltinEntityTypeNames.SqlEntityTypeName,
                Name = et.Name,
                ClrType = et,
                Attributes = attributes,
                PropertyInfos = new List<EntityPropertyInfo>()
            };
            this.PopulateProperties(entity);
            return entity;
        }

        private void PopulateProperties(EntityInfo entity) {
            var propInfos = entity.ClrType.GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.SetProperty | BindingFlags.GetProperty)
                .Where(x => this.IsEntityProperty(x));
            foreach (var pi in propInfos) {
                var epi = new EntityPropertyInfo {
                    Name = pi.Name,
                    ClrPropertyInfo = pi,
                    EntityInfo = entity,
                    Attributes = pi.GetCustomAttributes(),
                };
                entity.PropertyInfos.Add(epi);
            }
        }

        protected bool IsEntityType(Type t) =>
            t.IsClass && !t.IsAbstract && t.GetCustomAttribute<EntityAttribute>() != null;

        private bool IsEntityProperty(PropertyInfo propertyInfo) =>
            propertyInfo.CanRead && propertyInfo.CanWrite && propertyInfo.GetCustomAttribute<NotFieldAttribute>() == null;

    }

}
