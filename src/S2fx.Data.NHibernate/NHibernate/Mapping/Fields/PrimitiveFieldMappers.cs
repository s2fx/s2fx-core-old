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
            MetaField field) {
            var primitiveProperty = (PrimitiveMetaField)field;
            var mappingAction = new Action<IPropertyMapper>(mapper => {
                mapper.Column(field.DbName);
                mapper.NotNullable(primitiveProperty.IsRequired);
            });
            var next = new PropertyPath(currentPropertyPath, field.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(field.ClrPropertyInfo);
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
            MetaField field) {
            var primitiveProperty = (PrimitiveMetaField)field;
            var mappingAction = new Action<IPropertyMapper>(mapper => {
                if (field.MaxLength != null && field.MaxLength > 0) {
                    mapper.Length(field.MaxLength.Value);
                }
                else {
                    mapper.Type(NHibernateUtil.StringClob);
                }
                mapper.Column(field.DbName);
                mapper.NotNullable(primitiveProperty.IsRequired);
            });
            var next = new PropertyPath(currentPropertyPath, field.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(field.ClrPropertyInfo);
        }
    }


    public class DecimalFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.DecimalTypeName;
    }

    public class DateFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.DateTypeName;

        public override void MapField(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaField field) {
            var primitiveField = (PrimitiveMetaField)field;
            var mappingAction = new Action<IPropertyMapper>(mapper => {
                mapper.Type(NHibernateUtil.Date);
                mapper.Column(field.DbName);
                mapper.NotNullable(primitiveField.IsRequired);
            });
            var next = new PropertyPath(currentPropertyPath, field.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(field.ClrPropertyInfo);
        }
    }


    public class TimeFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.TimeTypeName;

        public override void MapField(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaField field) {
            var primitiveField = (PrimitiveMetaField)field;
            var mappingAction = new Action<IPropertyMapper>(mapper => {
                mapper.Type(NHibernateUtil.TimeAsTimeSpan);
                mapper.Column(field.DbName);
                mapper.NotNullable(primitiveField.IsRequired);
            });
            var next = new PropertyPath(currentPropertyPath, field.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(field.ClrPropertyInfo);
        }
    }


    public class UtcDateTimeFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.DateTimeTypeName;

        public override void MapField(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath member,
            MetaEntity entity,
            MetaField field) {
            var primitiveField = (PrimitiveMetaField)field;
            var mappingAction = new Action<IPropertyMapper>(mapper => {
                mapper.Type(NHibernateUtil.UtcDateTime);
                mapper.Column(field.DbName);
                mapper.NotNullable(primitiveField.IsRequired);
            });
            var next = new PropertyPath(member, field.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(field.ClrPropertyInfo);
        }
    }


    public class ByteArrayFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.ByteArrayTypeName;

        public override void MapField(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentFieldPath,
            MetaEntity entity,
            MetaField field) {
            var primitiveProperty = (PrimitiveMetaField)field;
            var mappingAction = new Action<IPropertyMapper>(mapper => {
                if (field.MaxLength != null && field.MaxLength > 0) {
                    mapper.Length(field.MaxLength.Value);
                }
                else {
                    mapper.Type(NHibernateUtil.BinaryBlob);
                }
                mapper.Column(field.DbName);
                mapper.NotNullable(primitiveProperty.IsRequired);
            });
            var next = new PropertyPath(currentFieldPath, field.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(field.ClrPropertyInfo);
        }

    }

}
