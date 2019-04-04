using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using S2fx.Convention;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;

namespace S2fx.Data.NHibernate.Mapping.Properties {

    public abstract class AbstractPrimitiveFieldMapper : AbstractFieldMapper {

        public override void MapField(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaField property) {
            var primitiveProperty = (PrimitiveMetaField)property;
            var mappingAction = new Action<global::NHibernate.Mapping.ByCode.IPropertyMapper>(mapper => {
                mapper.Column(property.DbName);
                mapper.NotNullable(primitiveProperty.IsRequired);
            });
            var next = new PropertyPath(currentPropertyPath, property.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(property.ClrPropertyInfo);
        }
    }

    public class BooleanFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.BooleanTypeName;
    }

    public class Int32FieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.Int32TypeName;
    }

    public class Int64FieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.Int64TypeName;
    }

    public class FloatFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.FloatTypeName;
    }

    public class StringFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.StringTypeName;

        public override void MapField(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaField property) {
            var primitiveProperty = (PrimitiveMetaField)property;
            var mappingAction = new Action<global::NHibernate.Mapping.ByCode.IPropertyMapper>(mapper => {
                if (property.MaxLength != null && property.MaxLength > 0) {
                    mapper.Length(property.MaxLength.Value);
                }
                else {
                    mapper.Type(NHibernateUtil.StringClob);
                }
                mapper.Column(property.DbName);
                mapper.NotNullable(primitiveProperty.IsRequired);
            });
            var next = new PropertyPath(currentPropertyPath, property.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(property.ClrPropertyInfo);
        }
    }

    public class PasswordFieldMapper : StringFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.Password;

    }

    public class DecimalFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.DecimalTypeName;
    }

    public class DateTimeFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.DateTimeTypeName;

        public override void MapField(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaField property) {
            base.MapField(customizerHolder, modelExplicitDeclarationsHolder, currentPropertyPath, entity, property);
        }
    }

    public class TimeSpanFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.TimeTypeName;
    }

    public class ByteArrayFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.ByteArrayTypeName;

        public override void MapField(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentFieldPath,
            MetaEntity entity,
            MetaField property) {
            var primitiveProperty = (PrimitiveMetaField)property;
            var mappingAction = new Action<global::NHibernate.Mapping.ByCode.IPropertyMapper>(mapper => {
                if (property.MaxLength != null && property.MaxLength > 0) {
                    mapper.Length(property.MaxLength.Value);
                }
                else {
                    mapper.Type(NHibernateUtil.BinaryBlob);
                }
                mapper.Column(property.DbName);
                mapper.NotNullable(primitiveProperty.IsRequired);
            });
            var next = new PropertyPath(currentFieldPath, property.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(property.ClrPropertyInfo);
        }

    }
}
