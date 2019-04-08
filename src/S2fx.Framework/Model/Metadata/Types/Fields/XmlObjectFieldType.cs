using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Metadata.Types {

    public interface IXmlObjectFieldType : IFieldType {
    }

    public class XmlObjectFieldType : AbstractFieldType, IXmlObjectFieldType {

        public override string Name => BuiltinFieldTypeNames.XmlObjectTypeName;

        public override bool SelectDefaultValue => false;
        public override bool UniqueDefaultValue => false;
        public override bool LazyDefaultValue => true;

        public override MetaField LoadClrProperty(PropertyInfo propertyInfo) {
            var jsonAttr = propertyInfo.GetCustomAttribute<XmlObjectFieldAttribute>();
            return new XmlMetaField {
                Name = propertyInfo.Name,
                DisplayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? propertyInfo.Name,
                Type = this,
                Attributes = propertyInfo.GetCustomAttributes(),
                ClrPropertyInfo = propertyInfo,
                MaxLength = -1,
            };
        }

        public override bool TryParse(MetaField property, string value, out object result, string format = null) {
            throw new NotImplementedException();
        }
    }

}
