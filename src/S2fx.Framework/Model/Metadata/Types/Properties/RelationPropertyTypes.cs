using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Convention;
using S2fx.Model.Annotations;

namespace S2fx.Model.Metadata.Types {

    public interface IRelationPropertyType : IPropertyType {
    }

    public abstract class AbstractRelationPropertyType : AbstractPropertyType, IRelationPropertyType {

        protected void ValidateCollectionPropertyType(PropertyInfo propertyInfo, string refEntityName) {
            if (!propertyInfo.PropertyType.IsGenericType || propertyInfo.PropertyType.GetGenericTypeDefinition() != typeof(ICollection<>)) {
                throw new EntityDefinitionException($"The property '{propertyInfo.Name}' of entity '{refEntityName}' must be a ICollection<T>");
            }
        }
    }

    public class ManyToOnePropertyType : AbstractRelationPropertyType {

        public override string Name => "ManyToOne";

        public override MetaProperty LoadClrProperty(PropertyInfo propertyInfo) {
            var manyToOneAttr = propertyInfo.GetCustomAttribute<ManyToOnePropertyAttribute>();
            var mappedByPropertyName = manyToOneAttr.MappedBy ?? "Id";
            var refEntityClrType = propertyInfo.PropertyType;
            var refEntityAttr = refEntityClrType.GetCustomAttribute<EntityAttribute>();
            return new ManyToOneMetaProperty {
                Name = propertyInfo.Name,
                DisplayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? propertyInfo.Name,
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
                MaxLength = -1,
                MappedByPropertyName = mappedByPropertyName,
                RefEntityName = manyToOneAttr.RefEntity ?? refEntityAttr.Name,
            };
        }

    }

    public class OneToManyPropertyType : AbstractRelationPropertyType {

        public override string Name => "OneToMany";

        public override MetaProperty LoadClrProperty(PropertyInfo propertyInfo) {
            var oneToManyAttr = propertyInfo.GetCustomAttribute<OneToManyPropertyAttribute>();
            var refEntityClrType = propertyInfo.PropertyType.GetGenericArguments().First();
            var refEntityAttr = refEntityClrType.GetCustomAttribute<EntityAttribute>();
            var refEntityName = oneToManyAttr.RefEntity ?? refEntityAttr.Name;
            this.ValidateCollectionPropertyType(propertyInfo, refEntityName);
            return new OneToManyMetaProperty {
                Name = propertyInfo.Name,
                DisplayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? propertyInfo.Name,
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
                MaxLength = -1,
                MappedByPropertyName = oneToManyAttr.MappedBy,
                RefEntityName = refEntityName,
            };
        }

        /*
        protected MetaEntity InferRefEntity(MetaProperty property) {
            var clrType = property.ClrPropertyInfo.PropertyType;
            if (clrType.IsGenericType && clrType.GetGenericTypeDefinition() == typeof(ICollection<>)) {
                return this.Entities.GetEntityByClrType(clrType.GetGenericArguments().First());
            }
            else {
                return this.Entities.GetEntityByClrType(clrType);
            }
        }
        */
    }

    public class ManyToManyPropertyType : AbstractRelationPropertyType {
        public override string Name => "ManyToMany";

        public override MetaProperty LoadClrProperty(PropertyInfo propertyInfo) {
            var thisEntityName = propertyInfo.DeclaringType.GetCustomAttribute<EntityAttribute>().Name;
            var manyToManyAttr = propertyInfo.GetCustomAttribute<ManyToManyPropertyAttribute>();
            var refEntityClrType = propertyInfo.PropertyType.GetGenericArguments().First();
            var refEntityAttr = refEntityClrType.GetCustomAttribute<EntityAttribute>();
            var refEntityName = manyToManyAttr.RefEntity ?? refEntityAttr.Name;
            var mappedByPropertyName = manyToManyAttr.MappedBy;
            var joinTable = manyToManyAttr.JoinTable;
            this.ValidateCollectionPropertyType(propertyInfo, refEntityName);
            return new ManyToManyMetaProperty {
                Name = propertyInfo.Name,
                DisplayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? propertyInfo.Name,
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
                MaxLength = -1,
                MappedByPropertyName = mappedByPropertyName,
                RefEntityName = refEntityName,
                JoinTable = joinTable,
            };
        }
    }
}
