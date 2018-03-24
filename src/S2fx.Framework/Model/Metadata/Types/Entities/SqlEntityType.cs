using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Environment.Extensions.Entity;
using S2fx.Model.Annotations;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Types {

    public class SqlEntityType : IEntityType {

        public string Name => BuiltinEntityTypeNames.SqlEntityTypeName;

        private readonly IEnumerable<IPropertyType> _propertyTypes;

        public SqlEntityType(IEnumerable<IPropertyType> types) {
            _propertyTypes = types;
        }

        public override int GetHashCode() =>
            this.Name.GetHashCode();

        public Task<MetaEntity> LoadAsync(EntityInfo descriptor) {
            var entityType = descriptor.ClrType;
            var entityAttribute = entityType.GetCustomAttribute<EntityAttribute>() ?? throw new InvalidOperationException();
            var displayName = entityType.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? entityAttribute.Name;
            var entity = new MetaEntity() {
                Name = entityAttribute.Name,
                DisplayName = displayName,
                ClrType = entityType,
                Attributes = entityType.GetCustomAttributes()
            };

            var allPropertyTypes = _propertyTypes;
            foreach (var entityPropertyInfo in descriptor.PropertyInfos) {
                var property = this.LoadPropertyByClr(entityPropertyInfo.ClrPropertyInfo, allPropertyTypes);
                property.Entity = entity;
                entity.Properties.Add(property.Name, property);
            }

            return Task.FromResult(entity);
        }

        private MetaProperty LoadPropertyByClr(PropertyInfo clrPropertyInfo, IEnumerable<IPropertyType> allPropertyTypes) {

            var propType = this.InferPropertyType(clrPropertyInfo, allPropertyTypes);

            return propType.LoadClrProperty(clrPropertyInfo);
        }

        private IPropertyType InferPropertyType(PropertyInfo clrPropertyInfo, IEnumerable<IPropertyType> allPropertyTypes) {
            var clrType = clrPropertyInfo.PropertyType;
            var propAttr = clrPropertyInfo.GetCustomAttributes()
                .SingleOrDefault(a => a.GetType().IsImplementsClass<AbstractPropertyAttribute>())
                    as AbstractPropertyAttribute;

            var primitiveTypes = allPropertyTypes.Where(t => t is IPrimitivePropertyType).Cast<IPrimitivePropertyType>();
            var enumerableType = allPropertyTypes.Single(t => t.Name == BuiltinPropertyTypeNames.EnumerableTypeName);

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
                return allPropertyTypes.Single(x => x.Name == propAttr.PropertyTypeName);
            }
            else {
                throw new EntityDefinitionException(
                    $"Invalid property type [{clrType.FullName}] in entity [{clrPropertyInfo.DeclaringType.FullName}#{clrPropertyInfo.Name}]");
            }
        }

    }

}
