using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using S2fx.Model.Metadata;

namespace S2fx.Data.NHibernate.Mapping.Properties {

    public interface IPropertyMapper {

        string PropertyTypeName { get; }

        void MapProperty(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaProperty property);
    }

}
