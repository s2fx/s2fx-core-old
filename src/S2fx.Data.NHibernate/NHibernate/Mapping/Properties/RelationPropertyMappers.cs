using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using NHibernate.Mapping.ByCode.Impl.CustomizersImpl;
using S2fx.Data.Convention;
using S2fx.Model;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;

namespace S2fx.Data.NHibernate.Mapping.Properties {

    public class ManyToOnePropertyMapper : AbstractPropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.ManyToOneTypeName;

        public ManyToOnePropertyMapper(IDbNameConvention nameConvention) : base(nameConvention) { }

        public override void MapProperty(ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaProperty property) {
            var m2oProperty = property as ManyToOneMetaProperty;
            var mappingAction = new Action<IManyToOneMapper>(mapper => {
                mapper.Column(this.NameConvention.EntityPropertyToColumn(property.Name));
                mapper.NotNullable(m2oProperty.IsRequired);
            });
            var next = new PropertyPath(currentPropertyPath, property.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsManyToOneRelation(property.ClrPropertyInfo);
        }
    }

    public class OneToManyPropertyMapper : AbstractPropertyMapper {
        private readonly IEntityManager _entityManager;

        public override string PropertyTypeName => BuiltinPropertyTypeNames.OneToManyTypeName;

        public OneToManyPropertyMapper(IDbNameConvention nameConvention, IEntityManager entityManager) : base(nameConvention) {
            _entityManager = entityManager;
        }

        public override void MapProperty(ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaProperty property) {

            var o2mProperty = property as OneToManyMetaProperty;
            var refEntity = _entityManager.GetEntity(o2mProperty.RefEntityName);
            var refProperty = refEntity.Properties[o2mProperty.MappedByPropertyName];

            var bagMappingAction = new Action<IBagPropertiesMapper>(mapper => {
                mapper.Inverse(true);
                mapper.Key(keyMapper => {
                    keyMapper.Column(this.NameConvention.EntityPropertyToColumn(refProperty.Name));
                });
            });

            var o2mMappingAction = new Action<IOneToManyMapper>(mapper => {
                mapper.Class(refEntity.ClrType);
            });

            var next = new PropertyPath(currentPropertyPath, property.ClrPropertyInfo);

            customizerHolder.AddCustomizer(next, o2mMappingAction);
            customizerHolder.AddCustomizer(next, bagMappingAction);

            modelExplicitDeclarationsHolder.AddAsOneToManyRelation(property.ClrPropertyInfo);
            modelExplicitDeclarationsHolder.AddAsBag(property.ClrPropertyInfo);

        }
    }

}
