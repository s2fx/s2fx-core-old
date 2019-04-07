using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using NHibernate.Mapping.ByCode.Impl.CustomizersImpl;
using S2fx.Conventions;
using S2fx.Model;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;

namespace S2fx.Data.NHibernate.Mapping.Properties {

    public class ManyToOneFieldMapper : AbstractFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.ManyToOneTypeName;

        public override void MapField(ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaField property) {
            var m2oProperty = property as ManyToOneMetaField;
            var mappingAction = new Action<IManyToOneMapper>(mapper => {
                mapper.Column(property.DbName);
                mapper.NotNullable(m2oProperty.IsRequired);
            });
            var next = new PropertyPath(currentPropertyPath, property.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsManyToOneRelation(property.ClrPropertyInfo);
        }
    }

    public class OneToManyFieldMapper : AbstractFieldMapper {
        private readonly IEntityManager _entityManager;

        public override string FieldTypeName => BuiltinFieldTypeNames.OneToManyTypeName;

        public OneToManyFieldMapper(IEntityManager entityManager) {
            _entityManager = entityManager;
        }

        public override void MapField(ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaField property) {

            var o2mProperty = property as OneToManyMetaField;
            var refEntity = _entityManager.GetEntity(o2mProperty.RefEntityName);
            var refProperty = refEntity.Fields[o2mProperty.MappedByFieldName];

            var bagMappingAction = new Action<IBagPropertiesMapper>(mapper => {
                mapper.Inverse(true);
                mapper.Key(keyMapper => {
                    keyMapper.Column(property.DbName);
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


    public class ManyToManyFieldMapper : AbstractFieldMapper {
        private readonly IEntityManager _entityManager;

        public override string FieldTypeName => BuiltinFieldTypeNames.ManyToManyTypeName;
        private readonly IDbNameConvention _nameConvention;

        public ManyToManyFieldMapper(IEntityManager entityManager, IDbNameConvention nameConvention) {
            _entityManager = entityManager;
            _nameConvention = nameConvention;
        }

        public override void MapField(ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaField property) {


            var m2mProperty = property as ManyToManyMetaField;
            var refEntity = _entityManager.GetEntity(m2mProperty.RefEntityName);
            var refProperty = refEntity.Fields[m2mProperty.MappedByFieldName];
            var joinTableThisSideFkColumn = _nameConvention.EntityPropertyToColumn(entity.Name.Split('.').Last() + "Id");
            var joinTableOtherSideFkColumn = _nameConvention.EntityPropertyToColumn(refEntity.Name.Split('.').Last() + "Id");

            var bagMappingAction = new Action<IBagPropertiesMapper>(mapper => {
                mapper.Table(m2mProperty.JoinTable);
                mapper.Key(keyMapper => {
                    keyMapper.Column(joinTableThisSideFkColumn);
                    keyMapper.NotNullable(true);
                });
            });

            var m2mMappingAction = new Action<IManyToManyMapper>(mapper => {
                mapper.Class(refEntity.ClrType);
                mapper.Column(joinTableOtherSideFkColumn);
            });

            var next = new PropertyPath(currentPropertyPath, property.ClrPropertyInfo);

            customizerHolder.AddCustomizer(next, m2mMappingAction);
            customizerHolder.AddCustomizer(next, bagMappingAction);

            modelExplicitDeclarationsHolder.AddAsManyToManyItemRelation(property.ClrPropertyInfo);
            modelExplicitDeclarationsHolder.AddAsBag(property.ClrPropertyInfo);

        }
    }

}
