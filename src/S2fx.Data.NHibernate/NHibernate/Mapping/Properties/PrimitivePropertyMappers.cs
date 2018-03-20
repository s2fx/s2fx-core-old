using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using S2fx.Data.Convention;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;

namespace S2fx.Data.NHibernate.Mapping.Properties {

    public abstract class AbstractPrimitivePropertyMapper : AbstractPropertyMapper {

        public AbstractPrimitivePropertyMapper(IDbNameConvention dbNameConvention)
            : base(dbNameConvention) {
        }

        public override void MapProperty(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaProperty property) {
            var mappingAction = new Action<global::NHibernate.Mapping.ByCode.IPropertyMapper>(mapper => {
                mapper.Column(this.NameConvention.EntityPropertyToColumn(property.Name));
            });
            var next = new PropertyPath(currentPropertyPath, property.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(property.ClrPropertyInfo);
        }
    }

    public class BooleanPropertyMapper : AbstractPrimitivePropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.BooleanTypeName;
        public BooleanPropertyMapper(IDbNameConvention nameConvention) : base(nameConvention) { }
    }

    public class Int32PropertyMapper : AbstractPrimitivePropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.Int32TypeName;
        public Int32PropertyMapper(IDbNameConvention nameConvention) : base(nameConvention) { }
    }

    public class Int64PropertyMapper : AbstractPrimitivePropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.Int64TypeName;
        public Int64PropertyMapper(IDbNameConvention nameConvention) : base(nameConvention) { }
    }

    public class FloatPropertyMapper : AbstractPrimitivePropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.FloatTypeName;
        public FloatPropertyMapper(IDbNameConvention nameConvention) : base(nameConvention) { }
    }

    public class StringPropertyMapper : AbstractPrimitivePropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.StringTypeName;
        public StringPropertyMapper(IDbNameConvention nameConvention) : base(nameConvention) { }

        public override void MapProperty(ICustomizersHolder customizerHolder, IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder, PropertyPath currentPropertyPath, MetaProperty property) {
            var mappingAction = new Action<global::NHibernate.Mapping.ByCode.IPropertyMapper>(mapper => {
                if (property.Length > 0) {
                    mapper.Length(property.Length);
                }
                else {
                    mapper.Length(int.MaxValue);
                }
                mapper.Column(this.NameConvention.EntityPropertyToColumn(property.Name));
            });
            var next = new PropertyPath(currentPropertyPath, property.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(property.ClrPropertyInfo);
        }
    }

    public class DateTimePropertyMapper : AbstractPrimitivePropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.DateTimeTypeName;
        public DateTimePropertyMapper(IDbNameConvention nameConvention) : base(nameConvention) { }
    }

    public class ByteArrayPropertyMapper : AbstractPrimitivePropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.ByteArrayTypeName;
        public ByteArrayPropertyMapper(IDbNameConvention nameConvention) : base(nameConvention) { }

        public override void MapProperty(ICustomizersHolder customizerHolder, IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder, PropertyPath currentPropertyPath, MetaProperty property) {
            var mappingAction = new Action<global::NHibernate.Mapping.ByCode.IPropertyMapper>(mapper => {
                if (property.Length > 0) {
                    mapper.Length(property.Length);
                }
                else {
                    mapper.Length(int.MaxValue);
                }
                mapper.Column(this.NameConvention.EntityPropertyToColumn(property.Name));
            });
            var next = new PropertyPath(currentPropertyPath, property.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(property.ClrPropertyInfo);
        }

    }
}
