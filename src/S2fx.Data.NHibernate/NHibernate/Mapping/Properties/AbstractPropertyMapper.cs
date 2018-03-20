using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using S2fx.Data.Convention;
using S2fx.Model.Metadata;

namespace S2fx.Data.NHibernate.Mapping.Properties {

    public abstract class AbstractPropertyMapper : IPropertyMapper {
        protected IDbNameConvention NameConvention { get; }

        public abstract string PropertyTypeName { get; }

        public AbstractPropertyMapper(IDbNameConvention dbNameConvention) {
            this.NameConvention = dbNameConvention;
        }

        public abstract void MapProperty(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaProperty property);
    }

}
