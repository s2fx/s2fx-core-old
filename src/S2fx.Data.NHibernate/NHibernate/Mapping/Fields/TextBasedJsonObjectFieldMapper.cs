using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using NHibernate.Type;
using S2fx.Data.NHibernate.Types;
using S2fx.Model;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;

namespace S2fx.Data.NHibernate.Mapping.Fields {

    public class TextBasedJsonObjectFieldMapper : AbstractFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.JsonObjectTypeName;

        public override void MapField(ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaField field) {
            var mappingAction = new Action<IPropertyMapper>(mapper => {
                var userType = MakeTextBasedJsonType(field.ClrPropertyInfo.PropertyType);
                mapper.Type(userType, null);
                mapper.Unique(false);
                mapper.Lazy(field.IsLazy);
                mapper.Column(field.DbName);
                mapper.NotNullable(field is IMetaFieldWithIsRequired fr && fr.IsRequired);
                if (field.MaxLength != null && field.MaxLength > 0) {
                    mapper.Length(field.MaxLength.Value);
                }
                else {
                    mapper.Length(8 * 1024);
                }
                mapper.Lazy(field.IsLazy);
            });
            var next = new PropertyPath(currentPropertyPath, field.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(field.ClrPropertyInfo);
        }

        private static Type MakeTextBasedJsonType(Type propertyType) {
            var genericType = typeof(TextBasedJsonObjectType<>);
            return genericType.MakeGenericType(propertyType);
        }
    }
}
