using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using OrchardCore.Environment.Extensions;
using OrchardCore.Modules;
using S2fx.Model.Annotations;
using S2fx.Model.Metadata;
using System.ComponentModel;
using S2fx.Model.Metadata.Loaders;
using S2fx.Model.Metadata.Types;
using S2fx.Data.Convention;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using S2fx.Model.Entities;

namespace S2fx.Environment.Extensions.Entity {

    public class ClrTypeModuleEntityInspector : IModuleEntityInspector {

        private readonly IHostingEnvironment _environment;
        private readonly IDbNameConvention _nameConvention;
        private readonly IClrSqlEntityType _clrSqlEntityType;

        public ILogger<ClrTypeModuleEntityInspector> Logger { get; }

        public ClrTypeModuleEntityInspector(
            IHostingEnvironment environment,
            IDbNameConvention nameConvention,
            IEnumerable<IEntityType> entityTypes,
            ILogger<ClrTypeModuleEntityInspector> logger) {
            _environment = environment;
            _nameConvention = nameConvention;
            _clrSqlEntityType = (IClrSqlEntityType)entityTypes.Single(x => x.Name == BuiltinEntityTypeNames.ClrSqlEntityTypeName);
            this.Logger = logger;
        }

        public Task<IEnumerable<EntityDescriptor>> InspectEntitiesAsync(string moduleId) {
            var module = _environment.GetModule(moduleId);
            var moduleName = module.ModuleInfo.Name;
            var assembly = module.Assembly;
            var entityTypes = this.InspectEntitiesInAssembly(assembly);
            if (moduleId == WellKnownConstants.CoreModuleId) {
                entityTypes = entityTypes.Concat(this.InspectEntitiesInAssembly(typeof(UserEntity).Assembly));
            }

            var entities = new List<EntityDescriptor>();
            foreach (var entityClrType in entityTypes) {
                if (!entityClrType.IsClass || !entityClrType.IsPublic || entityClrType.IsAbstract) {
                    throw new InvalidOperationException($"The entity `{entityClrType.FullName}` must be a non-abstract public class and must have the Entity attribute");
                }
                var entityAttr = entityClrType.GetCustomAttribute<EntityAttribute>();
                var entityName = entityAttr.Name ?? _nameConvention.EntityClrTypeNameToEntity(moduleName, entityClrType.Name);
                var descriptor = new EntityDescriptor(moduleName, entityName, _clrSqlEntityType, new string[] { }, entityClrType);
                entities.Add(descriptor);
                if (this.Logger.IsEnabled(LogLevel.Information)) {
                    this.Logger.LogInformation($"Found entity [{entityName}] in module [{moduleName}]");
                }
            }

            return Task.FromResult<IEnumerable<EntityDescriptor>>(entities);
        }

        private IEnumerable<Type> InspectEntitiesInAssembly(Assembly assembly) {
            var entityTypes = assembly.ExportedTypes
                .Where(t => t.GetCustomAttribute<EntityAttribute>() != null);
            return entityTypes;
        }

    }

}
