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

namespace S2fx.Model.Environment {

    public abstract class AbstractClrEntityHarvester : IEntityHarvester {

        public abstract int Priority { get; }
        public abstract Task<IEnumerable<EntityEntry>> HarvestEntitiesAsync();

        protected IEnumerable<EntityEntry> HarvestClrEntityInFeature(FeatureEntry feature) {
            var entityTypes = feature.ExportedTypes.Where(x => this.IsEntityType(x));

            foreach (var et in entityTypes) {
                var entity = GetEntityInfo(feature.FeatureInfo, et);
                yield return entity;
            }
        }

        protected EntityEntry GetEntityInfo(IFeatureInfo feature, Type et) {
            var entityAttr = et.GetCustomAttribute<EntityAttribute>();
            var attributes = et.GetCustomAttributes();
            var entity = new EntityEntry {
                Feature = feature,
                EntityType = BuiltinEntityTypeNames.SqlEntityTypeName,
                Name = et.Name,
                ClrType = et,
                Attributes = attributes
            };
            return entity;
        }

        protected bool IsEntityType(Type t) =>
            t.IsClass && !t.IsAbstract && t.GetCustomAttribute<EntityAttribute>() != null;
    }

}
