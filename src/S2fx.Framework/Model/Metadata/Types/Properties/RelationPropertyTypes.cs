using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Model.Annotations;

namespace S2fx.Model.Metadata.Types {

    public interface IRelationPropertyType : IPropertyType {

    }

    public abstract class AbstractRelationPropertyType : AbstractPropertyType, IRelationPropertyType {

        protected IServiceProvider Services { get; }
    }

    public class ManyToOnePropertyType : AbstractRelationPropertyType {

        public override string Name => "ManyToOne";

        public override MetaProperty LoadClrProperty(PropertyInfo propertyInfo) {
            var requiredAttr = propertyInfo.GetCustomAttribute<RequiredAttribute>();
            var manyToOneAttr = propertyInfo.GetCustomAttribute<ManyToOnePropertyAttribute>();
            var mappedByPropertyName = manyToOneAttr.MappedBy ?? "Id";
            return new ManyToOneMetaProperty {
                Name = propertyInfo.Name,
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
                IsRequired = requiredAttr != null ? true : false,
                Length = -1,
                MappedByPropertyName = mappedByPropertyName,
                RefEntityName = manyToOneAttr.RefEntity,
            };
        }

    }

    public class OneToManyPropertyType : AbstractRelationPropertyType {

        public override string Name => "OneToMany";

        public override MetaProperty LoadClrProperty(PropertyInfo propertyInfo) {
            var oneToManyAttr = propertyInfo.GetCustomAttribute<OneToManyPropertyAttribute>();
            return new OneToManyMetaProperty {
                Name = propertyInfo.Name,
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
                Length = -1,
                MappedByPropertyName = oneToManyAttr.MappedBy,
                RefEntityName = oneToManyAttr.RefEntity,
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
            throw new NotImplementedException();
        }
    }
}
