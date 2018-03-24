using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using S2fx.Convention;
using S2fx.Model;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;

namespace S2fx.Data.NHibernate.Mapping.Properties {

    public class IdPropertyMapper : AbstractPropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.IdTypeName;

        public override void MapProperty(ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaProperty property) {
            var mappingAction = new Action<IClassMapper>(mapper => {
                mapper.Id(idMapper => {
                    idMapper.Column(property.DbName);
                });
            });
            var next = new PropertyPath(currentPropertyPath, property.ClrPropertyInfo);
            customizerHolder.AddCustomizer(property.ClrPropertyInfo.DeclaringType, mappingAction);
            modelExplicitDeclarationsHolder.AddAsPoid(property.ClrPropertyInfo);
        }
    }
}
