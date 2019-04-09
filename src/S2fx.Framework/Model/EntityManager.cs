using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Environment.Extensions.Entity;
using System.Linq;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;
using S2fx.Model.Metadata.Conventions;
using Microsoft.Extensions.Logging;

namespace S2fx.Model {

    public class EntityManager : IEntityManager {

        private readonly IServiceProvider _services;
        private readonly Dictionary<string, MetaEntity> _entities =
            new Dictionary<string, MetaEntity>();
        private readonly IList<EntityInfo> _entityInfos = new List<EntityInfo>();
        private readonly object InitializationLock = new object();
        private bool _isLoaded = false;

        public ILogger Logger { get; }

        public EntityManager(IServiceProvider services, ILogger<EntityManager> logger) {
            _services = services;
            this.Logger = logger;
        }


        public IReadOnlyDictionary<string, MetaEntity> GetEntities() {
            this.EnsureInitialized();
            return _entities;
        }

        public MetaEntity GetEntity(string fullName) {
            if (string.IsNullOrEmpty(fullName)) {
                throw new ArgumentNullException(nameof(fullName));
            }
            this.EnsureInitialized();
            return _entities[fullName];
        }

        public MetaEntity GetEntityByClrType(Type entityType) {
            this.EnsureInitialized();
            return this.GetEntity(_entities.Single(pair => pair.Value.ClrType == entityType).Key);
        }

        public Task<IEnumerable<EntityInfo>> LoadEntitiesAsync() {
            this.EnsureInitialized();
            return Task.FromResult(_entityInfos.AsEnumerable());
        }

        private void EnsureInitialized() {
            if (_isLoaded) {
                return;
            }

            if (this.Logger.IsEnabled(LogLevel.Debug)) {
                Logger.LogDebug("Initializing EntityManager ...");
            }

            lock (this.InitializationLock) {
                var harvesters = _services.GetServices<IEntityHarvester>().OrderBy(x => x.Priority);
                var entityTypes = _services.GetServices<IEntityType>();

                var entityInfos = new List<EntityInfo>();
                foreach (var harvester in harvesters) {
                    var harvested = Task.Run(harvester.HarvestEntitiesAsync).Result;
                    entityInfos.AddRange(harvested);
                }

                this._entities.Clear();
                foreach (var entityInfo in entityInfos) {
                    var entityType = entityTypes.Single(x => x.Name == entityInfo.EntityType);

                    if (this.Logger.IsEnabled(LogLevel.Debug)) {
                        this.Logger.LogDebug("Found Entity '{0}' in feature '{1}'", entityInfo.Name, entityInfo.Feature.Id);
                    }

                    var metaEntity = Task.Run(() => entityType.LoadAsync(entityInfo)).Result;
                    if (!_entities.ContainsKey(metaEntity.Name)) {
                        _entities.Add(metaEntity.Name, metaEntity);
                    }
                }

                //处理约定
                var conventionVisitor = _services.GetRequiredService<ConventionMetadataVisitor>();
                foreach (var metaEntity in _entities.Values) {
                    conventionVisitor.VisitEntity(metaEntity);
                }

                _isLoaded = true;
            }
        }

    }

}
