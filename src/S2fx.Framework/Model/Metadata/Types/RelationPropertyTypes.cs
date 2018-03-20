using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Metadata.Types {

    public interface IRelationPropertyType : IPropertyType {
    }

    public abstract class AbstractRelationPropertyType : IRelationPropertyType {

        public abstract string Name { get; }

        public abstract MetaProperty ToMetaProperty(PropertyInfo propertyInfo);
    }

    public class ManyToOnePropertyType : AbstractRelationPropertyType {

        public override string Name => "ManyToOne";

        public override MetaProperty ToMetaProperty(PropertyInfo propertyInfo) {
            var requiredAttr = propertyInfo.GetCustomAttribute<RequiredAttribute>();
            var manyToOneAttr = propertyInfo.GetCustomAttribute<ManyToOnePropertyAttribute>();
            return new ManyToOneMetaProperty {
                Name = propertyInfo.Name,
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
                IsRequired = requiredAttr != null ? true : false,
                Length = -1,
                MappedBy = manyToOneAttr.MappedBy,
                RefEntity = manyToOneAttr.RefEntity,
            };
        }
    }

    public class OneToManyPropertyType : AbstractRelationPropertyType {

        public override string Name => "OneToMany";

        public override MetaProperty ToMetaProperty(PropertyInfo propertyInfo) {
            var oneToManyAttr = propertyInfo.GetCustomAttribute<OneToManyPropertyAttribute>();
            return new OneToManyMetaProperty {
                Name = propertyInfo.Name,
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
                Length = -1,
                MappedBy = oneToManyAttr.MappedBy,
                RefEntity = oneToManyAttr.RefEntity,
            };
        }
    }

    public class ManyToManyPropertyType : AbstractRelationPropertyType {
        public override string Name => "ManyToMany";

        public override MetaProperty ToMetaProperty(PropertyInfo propertyInfo) {
            throw new NotImplementedException();
        }
    }
}
