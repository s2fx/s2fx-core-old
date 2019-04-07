using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using S2fx.Model;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;

namespace S2fx.Data.NHibernate.Mapping.Properties {

    public class IdFieldMapper : AbstractFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.IdTypeName;

        public override void MapField(ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaField field) {
            var mappingAction = new Action<IClassMapper>(mapper => {
                mapper.Id(idMapper => {
                    idMapper.Column(field.DbName);
                });
            });
            var next = new PropertyPath(currentPropertyPath, field.ClrPropertyInfo);
            customizerHolder.AddCustomizer(field.ClrPropertyInfo.DeclaringType, mappingAction);
            modelExplicitDeclarationsHolder.AddAsPoid(field.ClrPropertyInfo);
        }
    }
}
