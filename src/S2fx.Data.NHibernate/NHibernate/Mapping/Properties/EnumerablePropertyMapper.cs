using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using NHibernate.Type;
using S2fx.Convention;
using S2fx.Model;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;

namespace S2fx.Data.NHibernate.Mapping.Properties {

    public class EnumerablePropertyMapper : AbstractPropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.EnumerableTypeName;

        public override void MapProperty(ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaProperty property) {
            var genericHibernateEnumTypeType = typeof(global::NHibernate.Type.EnumStringType<>);
            var hibernateEnumTypeType = genericHibernateEnumTypeType.MakeGenericType(property.ClrPropertyInfo.PropertyType);
            var hibernateEnumType = Activator.CreateInstance(hibernateEnumTypeType) as IType;

            var primitiveProperty = (EnumerableMetaProperty)property;
            var mappingAction = new Action<global::NHibernate.Mapping.ByCode.IPropertyMapper>(mapper => {
                mapper.Column(property.DbName);
                mapper.Type(hibernateEnumType);
                mapper.NotNullable(primitiveProperty.IsRequired);
            });
            var next = new PropertyPath(currentPropertyPath, property.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(property.ClrPropertyInfo);
        }
    }
}
