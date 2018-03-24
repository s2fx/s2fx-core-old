using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using S2fx.Convention;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;

namespace S2fx.Data.NHibernate.Mapping.Properties {

    public abstract class AbstractPrimitivePropertyMapper : AbstractPropertyMapper {

        public override void MapProperty(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaProperty property) {
            var primitiveProperty = (PrimitiveMetaProperty)property;
            var mappingAction = new Action<global::NHibernate.Mapping.ByCode.IPropertyMapper>(mapper => {
                mapper.Column(property.DbName);
                mapper.NotNullable(primitiveProperty.IsRequired);
            });
            var next = new PropertyPath(currentPropertyPath, property.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(property.ClrPropertyInfo);
        }
    }

    public class BooleanPropertyMapper : AbstractPrimitivePropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.BooleanTypeName;
    }

    public class Int32PropertyMapper : AbstractPrimitivePropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.Int32TypeName;
    }

    public class Int64PropertyMapper : AbstractPrimitivePropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.Int64TypeName;
    }

    public class FloatPropertyMapper : AbstractPrimitivePropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.FloatTypeName;
    }

    public class StringPropertyMapper : AbstractPrimitivePropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.StringTypeName;

        public override void MapProperty(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaProperty property) {
            var primitiveProperty = (PrimitiveMetaProperty)property;
            var mappingAction = new Action<global::NHibernate.Mapping.ByCode.IPropertyMapper>(mapper => {
                if (property.Length > 0) {
                    mapper.Length(property.Length);
                }
                else {
                    mapper.Length(int.MaxValue);
                }
                mapper.Column(property.DbName);
                mapper.NotNullable(primitiveProperty.IsRequired);
            });
            var next = new PropertyPath(currentPropertyPath, property.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(property.ClrPropertyInfo);
        }
    }

    public class DateTimePropertyMapper : AbstractPrimitivePropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.DateTimeTypeName;
    }

    public class ByteArrayPropertyMapper : AbstractPrimitivePropertyMapper {
        public override string PropertyTypeName => BuiltinPropertyTypeNames.ByteArrayTypeName;

        public override void MapProperty(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaProperty property) {
            var primitiveProperty = (PrimitiveMetaProperty)property;
            var mappingAction = new Action<global::NHibernate.Mapping.ByCode.IPropertyMapper>(mapper => {
                if (property.Length > 0) {
                    mapper.Length(property.Length);
                }
                else {
                    mapper.Length(int.MaxValue);
                }
                mapper.Column(property.DbName);
                mapper.NotNullable(primitiveProperty.IsRequired);
            });
            var next = new PropertyPath(currentPropertyPath, property.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(property.ClrPropertyInfo);
        }

    }
}
