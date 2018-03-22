using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using S2fx.Model.Annotations;
using System.ComponentModel;
using System.Linq;
using Microsoft.Extensions.Logging;
using S2fx.Utility;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Logging.Abstractions;
using S2fx.Model;
using S2fx.Model.Metadata.Types;

namespace S2fx.Model.Metadata.Loaders {

    public class ClrTypeEntityMetadataLoader : IClrTypeEntityMetadataLoader {
        private readonly IEnumerable<IPropertyType> _propertyTypes;

        private readonly Dictionary<Type, IPrimitivePropertyType> _primitivePropertyTypes =
            new Dictionary<Type, IPrimitivePropertyType>();

        public ILogger<ClrTypeEntityMetadataLoader> Logger { get; }

        public ClrTypeEntityMetadataLoader(
            IEnumerable<IPropertyType> propertyTypes,
            ILogger<ClrTypeEntityMetadataLoader> logger) {
            this.Logger = logger;

            _propertyTypes = propertyTypes;
            foreach (var pt in _propertyTypes) {
                if (pt is IPrimitivePropertyType ppt) {
                    _primitivePropertyTypes.Add(ppt.ClrType, ppt);
                }
            }

        }

        public MetaEntity LoadEntityByClr(Type entityType) {

            var entityAttribute = entityType.GetCustomAttribute<EntityAttribute>() ?? throw new InvalidOperationException();
            var displayName = entityType.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? entityAttribute.Name;
            var entity = new MetaEntity() {
                Name = entityAttribute.Name,
                DisplayName = displayName,
                ClrType = entityType,
                Attributes = entityType.GetCustomAttributes()
            };

            foreach (var clrPropertyInfo in entityType.GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.SetProperty | BindingFlags.GetProperty)) {

                if (clrPropertyInfo.GetCustomAttribute<NotMappedAttribute>() != null || !clrPropertyInfo.CanWrite || !clrPropertyInfo.CanRead) {
                    continue;
                }

                var property = this.LoadPropertyByClr(clrPropertyInfo);
                property.Entity = entity;
                entity.Properties.Add(property.Name, property);
            }

            return entity;
        }

        private MetaProperty LoadPropertyByClr(PropertyInfo clrPropertyInfo) {

            var propType = this.InferPropertyType(clrPropertyInfo);

            return propType.LoadClrProperty(clrPropertyInfo);
        }

        private IPropertyType InferPropertyType(PropertyInfo clrPropertyInfo) {
            var clrType = clrPropertyInfo.PropertyType;
            var propAttr = clrPropertyInfo.GetCustomAttributes()
                .SingleOrDefault(a => a.GetType().IsImplementsClass<AbstractPropertyAttribute>())
                    as AbstractPropertyAttribute;

            var isPrimitive = this.IsPrimitivePropertyType(clrType);

            if (propAttr == null && isPrimitive) {
                return clrType.IsConstructedGenericType && clrType.GetGenericTypeDefinition() == typeof(Nullable<>) ?
                    _primitivePropertyTypes[clrType.GetGenericArguments().First()] : _primitivePropertyTypes[clrType];
            }
            else if (propAttr != null) {
                return _propertyTypes.Single(x => x.Name == propAttr.PropertyTypeName);
            }
            else {
                throw new EntityDefinitionException(
                    $"Invalid property type [{clrType.FullName}] in entity [{clrPropertyInfo.DeclaringType.FullName}#{clrPropertyInfo.Name}]");
            }
        }

        private bool IsPrimitivePropertyType(Type t) =>
            this.IsNotNullablePrimitivePropertyType(t) || this.IsNullablePrimitivePropertyType(t);

        private bool IsNullablePrimitivePropertyType(Type t) =>
            t.IsConstructedGenericType
                && t.GetGenericTypeDefinition() == typeof(Nullable<>)
                && this.IsNotNullablePrimitivePropertyType(t.GetGenericArguments().First());

        private bool IsNotNullablePrimitivePropertyType(Type t) => _primitivePropertyTypes.ContainsKey(t);
    }

}
