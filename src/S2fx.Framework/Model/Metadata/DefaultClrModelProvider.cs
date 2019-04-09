using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using S2fx.Model.Annotations;
using S2fx.Model.Environment;
using S2fx.Model.Metadata.Types;
using S2fx.Utility;

namespace S2fx.Model.Metadata {

    public class DefaultClrModelProvider : IMetadataModelProvider {

        public int Order => -2000;

        readonly IEnumerable<IEntityType> _entityTypes;
        readonly IEnumerable<IFieldType> _fieldTypes;

        public ILogger Logger { get; }

        public DefaultClrModelProvider(IEnumerable<IEntityType> entityTypes, IEnumerable<IFieldType> fieldTypes, ILogger<DefaultClrModelProvider> logger) {
            _entityTypes = entityTypes;
            _fieldTypes = fieldTypes;
            this.Logger = logger;
        }

        public void OnProvidersExecuting(MetadataModelProviderContext context) {
            foreach (var entityEntry in context.EntityEntries) {
                if (this.Logger.IsEnabled(LogLevel.Debug)) {
                    this.Logger.LogDebug("Loading CLR entity '{0}' in feature '{1}'", entityEntry.Name, entityEntry.Feature.Id);
                }
                var metaEntity = this.CreateMetaEntity(entityEntry);
                //TODO 这里处理继承等问题
                context.Result.Entities.Add(metaEntity.Name, metaEntity);
            }

        }

        public void OnProvidersExecuted(MetadataModelProviderContext context) {
        }

        MetaEntity CreateMetaEntity(EntityEntry descriptor) {

            var entityType = descriptor.ClrType;
            var entityAttribute = entityType.GetCustomAttribute<EntityAttribute>() ?? throw new InvalidOperationException();
            var displayName = entityType.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? entityAttribute.Name;
            var entity = new MetaEntity() {
                Name = entityAttribute.Name,
                EntityType = _entityTypes.First(), //TODO FIXME
                DisplayName = displayName,
                ClrType = entityType,
                Feature = descriptor.Feature,
                Attributes = entityType.GetCustomAttributes(),
            };

            var propFilter = BindingFlags.Public
                | BindingFlags.FlattenHierarchy
                | BindingFlags.Instance
                | BindingFlags.GetProperty
                | BindingFlags.SetProperty;
            var props = descriptor.ClrType.GetProperties(propFilter).
                Where(x => x.CanRead && x.CanWrite);

            foreach (var propertyInfo in props) {

                var metaField = this.LoadPropertyByClr(propertyInfo, _fieldTypes);
                metaField.Entity = entity;
                metaField.Attributes = propertyInfo.GetCustomAttributes(true).Where(x => x is Attribute).Cast<Attribute>();
                entity.Fields.Add(metaField.Name, metaField);
            }
            return entity;
        }

        MetaField LoadPropertyByClr(PropertyInfo clrPropertyInfo, IEnumerable<IFieldType> allPropertyTypes) {

            var propType = this.InferPropertyType(clrPropertyInfo, allPropertyTypes);

            return propType.LoadClrProperty(clrPropertyInfo);
        }

        IFieldType InferPropertyType(PropertyInfo clrPropertyInfo, IEnumerable<IFieldType> allPropertyTypes) {
            var clrType = clrPropertyInfo.PropertyType;
            var propAttr = clrPropertyInfo.GetCustomAttributes()
                .SingleOrDefault(a => a.GetType().IsImplementsClass<AbstractFieldAttribute>())
                    as AbstractFieldAttribute;

            var primitiveTypes = allPropertyTypes.Where(t => t is IPrimitiveFieldType).Cast<IPrimitiveFieldType>();
            var enumerableType = allPropertyTypes.Single(t => t.Name == BuiltinFieldTypeNames.EnumerableTypeName);

            var primitiveType = clrType.IsNullableValueType() ?
                primitiveTypes.FirstOrDefault(pt => clrType.GetGenericArguments().First() == pt.ClrType)
                    : primitiveTypes.FirstOrDefault(pt => clrType == pt.ClrType);

            if (propAttr == null && primitiveType != null) {
                return primitiveType;
            }
            else if (propAttr == null && (clrType.IsEnum || (clrType.IsNullableValueType() && clrType.GetGenericArguments().First().IsEnum))) {
                //TODO required
                return enumerableType;
            }
            else if (propAttr != null) {
                return allPropertyTypes.Single(x => x.Name == propAttr.FieldTypeName);
            }
            else {
                throw new EntityDefinitionException(
                    $"Invalid property type [{clrType.FullName}] in entity [{clrPropertyInfo.DeclaringType.FullName}#{clrPropertyInfo.Name}]");
            }
        }
    }

}
