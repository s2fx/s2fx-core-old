using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Environment.Extensions.Entity;
using S2fx.Model.Metadata;
using System.Linq;

namespace S2fx.Model {

    public class EntityManager : IEntityManager {
        private readonly IServiceProvider _services;
        private readonly Dictionary<string, MetaEntity> _entities = new Dictionary<string, MetaEntity>();

        public EntityManager(IServiceProvider services) {
            _services = services;
        }

        public IEnumerable<MetaEntity> GetEnabledEntities() {
            this.EnsureEntitiesLoaded();
            return _entities.Values;
        }

        public MetaEntity GetEntity(string fullName) {
            this.EnsureEntitiesLoaded();
            return _entities[fullName];
        }

        public MetaEntity GetEntityByClrType(Type entityType) =>
            this.GetEntity(_entities.Single(pair => pair.Value.ClrType == entityType).Key);

        private void EnsureEntitiesLoaded() {
            if (_entities.Count == 0) {
                var entityHarvester = _services.GetService<IEntityHarvester>();
                var featureEntities = Task.Run(entityHarvester.HarvestEntitiesAsync).Result;
                lock (this) {
                    foreach (var fe in featureEntities) {
                        foreach (var entity in fe.Entities) {
                            if (!_entities.ContainsKey(entity.Name)) {
                                _entities.Add(entity.Name, entity);
                            }
                        }
                    }
                }
            }
        }

    }

}
