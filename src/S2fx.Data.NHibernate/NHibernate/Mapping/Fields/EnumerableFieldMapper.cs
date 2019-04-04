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

    public class EnumerableFieldMapper : AbstractFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.EnumerableTypeName;

        public override void MapField(ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaField field) {
            var genericHibernateEnumTypeType = typeof(global::NHibernate.Type.EnumStringType<>);
            var hibernateEnumTypeType = genericHibernateEnumTypeType.MakeGenericType(field.ClrPropertyInfo.PropertyType);
            var hibernateEnumType = Activator.CreateInstance(hibernateEnumTypeType) as IType;

            var primitiveField = (EnumerableMetaField)field;
            var mappingAction = new Action<global::NHibernate.Mapping.ByCode.IPropertyMapper>(mapper => {
                mapper.Column(field.DbName);
                mapper.Type(hibernateEnumType);
                mapper.NotNullable(primitiveField.IsRequired);
            });
            var next = new PropertyPath(currentPropertyPath, field.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(field.ClrPropertyInfo);
        }
    }
}
