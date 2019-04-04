using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using S2fx.Model.Metadata;

namespace S2fx.Data.NHibernate.Mapping.Properties {

    public interface IFieldMapper {

        string FieldTypeName { get; }

        void MapField(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentFieldPath,
            MetaEntity entity,
            MetaField field);
    }

}
