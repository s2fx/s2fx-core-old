using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using S2fx.Model.Annotations;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Conventions {

    public class BuiltinEntityFieldAttributeConvention : AbstractEntityFieldConvention {

        public override void Apply(MetaField field) {

            this.ApplyLazyAttribute(field);
            this.ApplyMaxLengthAttribute(field);
            this.ApplyReadOnlyAttribute(field);
            this.ApplyUniqueAttribute(field);
            this.ApplySelectAttribute(field);
            this.ApplyRequiredAttribute(field);
        }

        private void ApplyLazyAttribute(MetaField field) {
            field.IsLazy = field.ClrPropertyInfo.GetCustomAttribute<LazyAttribute>() != null;
        }


        private void ApplyMaxLengthAttribute(MetaField field) {
            var maxLengthAttr = field.ClrPropertyInfo.GetCustomAttribute<MaxLengthAttribute>();
            field.MaxLength = maxLengthAttr?.Length ?? -1;
        }

        private void ApplyReadOnlyAttribute(MetaField field) {
            field.IsReadOnly = field.ClrPropertyInfo.GetCustomAttribute<ReadOnlyAttribute>() != null;
        }

        private void ApplyUniqueAttribute(MetaField field) {
            var attr = field.ClrPropertyInfo.GetCustomAttribute<UniqueAttribute>();
            field.IsUnique = attr != null ? true : field.Type.UniqueDefaultValue;
        }


        private void ApplySelectAttribute(MetaField field) {
            var attr = field.ClrPropertyInfo.GetCustomAttribute<SelectAttribute>();
            field.IsSelect = attr != null ? true : field.Type.SelectDefaultValue;
        }

        private void ApplyRequiredAttribute(MetaField field) {
            if (field is IMetaFieldWithIsRequired withIsRequired) {
                var clrType = field.ClrPropertyInfo.PropertyType;
                withIsRequired.IsRequired =
                    field.ClrPropertyInfo.GetCustomAttribute<RequiredAttribute>() != null ? true : !clrType.IsNullableType();
            }
        }

    }

}
