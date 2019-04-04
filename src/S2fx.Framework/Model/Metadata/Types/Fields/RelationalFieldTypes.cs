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

    public interface IRelationalFieldType : IFieldType {
    }

    public abstract class AbstractRelationalFieldType : AbstractFieldType, IRelationalFieldType {

        protected void ValidateCollectionPropertyType(PropertyInfo propertyInfo, string refEntityName) {
            if (!propertyInfo.PropertyType.IsGenericType || propertyInfo.PropertyType.GetGenericTypeDefinition() != typeof(ICollection<>)) {
                throw new EntityDefinitionException($"The property '{propertyInfo.Name}' of entity '{refEntityName}' must be a ICollection<T>");
            }
        }

    }

    public class ManyToOneFieldType : AbstractRelationalFieldType {

        public override string Name => BuiltinFieldTypeNames.ManyToOneTypeName;

        public override MetaField LoadClrProperty(PropertyInfo propertyInfo) {
            var manyToOneAttr = propertyInfo.GetCustomAttribute<ManyToOneFieldAttribute>();
            var mappedByPropertyName = manyToOneAttr.MappedBy ?? "Id";
            var refEntityClrType = propertyInfo.PropertyType;
            var refEntityAttr = refEntityClrType.GetCustomAttribute<EntityAttribute>();
            return new ManyToOneMetaField {
                Name = propertyInfo.Name,
                DisplayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? propertyInfo.Name,
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
                MaxLength = -1,
                MappedByFieldName = mappedByPropertyName,
                RefEntityName = manyToOneAttr.RefEntity ?? refEntityAttr.Name,
            };
        }

        public override bool TryParse(MetaField property, string value, out object result, string format = null) {
            throw new NotSupportedException();
        }

    }

    public class OneToManyFieldType : AbstractRelationalFieldType {

        public override string Name => BuiltinFieldTypeNames.OneToManyTypeName;

        public override MetaField LoadClrProperty(PropertyInfo propertyInfo) {
            var oneToManyAttr = propertyInfo.GetCustomAttribute<OneToManyFieldAttribute>();
            var refEntityClrType = propertyInfo.PropertyType.GetGenericArguments().First();
            var refEntityAttr = refEntityClrType.GetCustomAttribute<EntityAttribute>();
            var refEntityName = oneToManyAttr.RefEntity ?? refEntityAttr.Name;
            this.ValidateCollectionPropertyType(propertyInfo, refEntityName);
            return new OneToManyMetaField {
                Name = propertyInfo.Name,
                DisplayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? propertyInfo.Name,
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
                MaxLength = -1,
                MappedByFieldName = oneToManyAttr.MappedBy,
                RefEntityName = refEntityName,
            };
        }

        public override bool TryParse(MetaField property, string value, out object result, string format = null) {
            throw new NotSupportedException();
        }
    }

    public class ManyToManyFieldType : AbstractRelationalFieldType {
        public override string Name => BuiltinFieldTypeNames.ManyToManyTypeName;

        public override MetaField LoadClrProperty(PropertyInfo propertyInfo) {
            var thisEntityName = propertyInfo.DeclaringType.GetCustomAttribute<EntityAttribute>().Name;
            var manyToManyAttr = propertyInfo.GetCustomAttribute<ManyToManyFieldAttribute>();
            var refEntityClrType = propertyInfo.PropertyType.GetGenericArguments().First();
            var refEntityAttr = refEntityClrType.GetCustomAttribute<EntityAttribute>();
            var refEntityName = manyToManyAttr.RefEntity ?? refEntityAttr.Name;
            var mappedByPropertyName = manyToManyAttr.MappedBy;
            var joinTable = manyToManyAttr.JoinTable;
            this.ValidateCollectionPropertyType(propertyInfo, refEntityName);
            return new ManyToManyMetaField {
                Name = propertyInfo.Name,
                DisplayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? propertyInfo.Name,
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
                MaxLength = -1,
                MappedByFieldName = mappedByPropertyName,
                RefEntityName = refEntityName,
                JoinTable = joinTable,
            };
        }

        public override bool TryParse(MetaField property, string value, out object result, string format = null) {
            throw new NotSupportedException();
        }
    }

}
