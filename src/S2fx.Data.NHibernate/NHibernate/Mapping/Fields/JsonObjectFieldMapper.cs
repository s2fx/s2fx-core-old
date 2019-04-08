using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using NHibernate.Type;
using S2fx.Data.NHibernate.DbProviders;
using S2fx.Data.NHibernate.Types;
using S2fx.Model;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;

namespace S2fx.Data.NHibernate.Mapping.Fields {

    public class JsonObjectFieldMapper : AbstractFieldMapper {
        readonly IHibernateDbProviderAccessor _providerAccessor;

        public override string FieldTypeName => BuiltinFieldTypeNames.JsonObjectTypeName;

        public JsonObjectFieldMapper(IHibernateDbProviderAccessor providerAccessor) {
            _providerAccessor = providerAccessor;
        }

        public override void MapField(ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaField field) {
            var mappingAction = new Action<IPropertyMapper>(mapper => {
                var userType = this.MakeTextBasedJsonType(field.ClrPropertyInfo.PropertyType);
                mapper.Type(userType, null);
                mapper.Unique(false);
                mapper.Lazy(field.IsLazy);
                mapper.Column(field.DbName);
                mapper.NotNullable(field is IMetaFieldWithIsRequired fr && fr.IsRequired);
                if (field.MaxLength != null && field.MaxLength > 0) {
                    mapper.Length(field.MaxLength.Value);
                }
            });
            var next = new PropertyPath(currentPropertyPath, field.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(field.ClrPropertyInfo);
        }

        private Type MakeTextBasedJsonType(Type propertyType) {
            var genericType = _providerAccessor.EnabledDbProvider.JsonObjectType;
            return genericType.MakeGenericType(propertyType);
        }
    }
}
