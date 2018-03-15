using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using S2fx.Environment.Extensions.Entity;
using S2fx.Model.Metadata;
using System.Linq;

namespace S2fx.Model {

    public class EntityManager : IEntityManager {
        private readonly IEntityHarvester _entityHarvester;
        private readonly Dictionary<string, IEnumerable<EntityInfo>> _entities = new Dictionary<string, IEnumerable<EntityInfo>>();

        public EntityManager(IEntityHarvester entityHarvester) {
            _entityHarvester = entityHarvester;
        }

        public IEnumerable<EntityInfo> GetEnabledEntities() {
            this.EnsureEntitiesLoaded();
            return _entities.SelectMany(e => e.Value);
        }

        public EntityInfo GetEntity(string moduleName, string entityName) {
            this.EnsureEntitiesLoaded();
            return _entities[moduleName].Single(x => x.Name == entityName);
        }

        private void EnsureEntitiesLoaded() {
            if (_entities.Count == 0) {
                var entities = Task.Run(_entityHarvester.HarvestEntitiesAsync).Result;
                lock (this) {
                    foreach (var entity in entities) {
                        _entities.Add(entity.Feature, entity.Entities);
                    }
                }
            }
        }

    }

}
