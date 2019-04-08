using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using NHibernate.Type;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;

namespace S2fx.Data.NHibernate.Mapping.Fields {

    public abstract class AbstractPrimitiveFieldMapper : AbstractFieldMapper {

        public abstract IType NHType { get; }

        public override void MapField(
            ICustomizersHolder customizerHolder,
            IModelExplicitDeclarationsHolder modelExplicitDeclarationsHolder,
            PropertyPath currentPropertyPath,
            MetaEntity entity,
            MetaField field) {
            var primitiveProperty = (PrimitiveMetaField)field;
            var mappingAction = new Action<IPropertyMapper>(mapper => {
                mapper.Type(this.NHType);
                mapper.Column(field.DbName);
                mapper.NotNullable(primitiveProperty.IsRequired);
                if (field.MaxLength != null && field.MaxLength > 0) {
                    mapper.Length(field.MaxLength.Value);
                }
                mapper.Unique(field.IsUnique);
                mapper.Lazy(field.IsLazy);
            });
            var next = new PropertyPath(currentPropertyPath, field.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(field.ClrPropertyInfo);
        }
    }

    public class BooleanFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.BooleanTypeName;
        public override IType NHType => NHibernateUtil.Boolean;
    }

    public class Int32FieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.Int32TypeName;
        public override IType NHType => NHibernateUtil.Int32;
    }

    public class Int64FieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.Int64TypeName;
        public override IType NHType => NHibernateUtil.Int64;
    }

    public class FloatFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.FloatTypeName;
        public override IType NHType => NHibernateUtil.Single;
    }

    public class StringFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.StringTypeName;
        public override IType NHType => throw new NotSupportedException();

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
                mapper.Unique(field.IsUnique);
                mapper.Lazy(field.IsLazy);
                mapper.NotNullable(primitiveProperty.IsRequired);
            });
            var next = new PropertyPath(currentPropertyPath, field.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(field.ClrPropertyInfo);
        }
    }


    public class DecimalFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.DecimalTypeName;
        public override IType NHType => NHibernateUtil.Decimal;
    }

    public class DateFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.DateTypeName;
        public override IType NHType => NHibernateUtil.Date;
    }


    public class TimeFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.TimeTypeName;
        public override IType NHType => NHibernateUtil.Time;
    }


    public class UtcDateTimeFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.DateTimeTypeName;
        public override IType NHType => NHibernateUtil.UtcDateTime;
    }


    public class ByteArrayFieldMapper : AbstractPrimitiveFieldMapper {
        public override string FieldTypeName => BuiltinFieldTypeNames.ByteArrayTypeName;
        public override IType NHType => NHibernateUtil.BinaryBlob;

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
                mapper.Unique(field.IsUnique);
                mapper.Lazy(field.IsLazy);
            });
            var next = new PropertyPath(currentFieldPath, field.ClrPropertyInfo);
            customizerHolder.AddCustomizer(next, mappingAction);
            modelExplicitDeclarationsHolder.AddAsProperty(field.ClrPropertyInfo);
        }

    }

}
