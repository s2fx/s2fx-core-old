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
using S2fx.Model.Metadata.Types;

namespace S2fx.Model {

    public interface IEntityManager {

        MetaEntity GetEntityByClrType(Type entityType);
        MetaEntity GetEntity(string fullName);
        IEnumerable<MetaEntity> GetEnabledEntities();

        Task LoadAsync();
    }

    public class EntityManager : IEntityManager {
        private readonly IServiceProvider _services;
        private readonly IEntityHarvester _harvester;
        private readonly ConcurrentDictionary<string, MetaEntity> _entities =
            new ConcurrentDictionary<string, MetaEntity>();

        public EntityManager(IServiceProvider services, IEntityHarvester entityHarvester) {
            _services = services;
            _harvester = entityHarvester;
        }

        public IEnumerable<MetaEntity> GetEnabledEntities() {
            this.EnsureEntitiesLoaded();
            return _entities.Values;
        }

        public MetaEntity GetEntity(string fullName) {
            if (string.IsNullOrEmpty(fullName)) {
                throw new ArgumentNullException(nameof(fullName));
            }
            this.EnsureEntitiesLoaded();
            return _entities[fullName];
        }

        public MetaEntity GetEntityByClrType(Type entityType) =>
            this.GetEntity(_entities.Single(pair => pair.Value.ClrType == entityType).Key);

        public async Task LoadAsync() {

            var entityDescriptors = await _harvester.HarvestEntitiesAsync();
            foreach (var descriptor in entityDescriptors) {
                if (!_entities.ContainsKey(descriptor.Name)) {
                    var entity = await descriptor.Type.LoadAsync(descriptor);
                    _entities.AddOrUpdate(descriptor.Name, entity, (x, y) => entity);
                }
            }
        }

        private void EnsureEntitiesLoaded() {
            if (_entities.Count == 0) {
                Task.Run(this.LoadAsync).Wait();
            }
        }

    }

}
