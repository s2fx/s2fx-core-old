using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace S2fx.Model.Metadata.Types {

    public class BooleanPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.BooleanTypeName;
        public override Type ClrType => typeof(bool);

        public override bool TryParsePropertyValue(MetaProperty property, string value, out object result, string format = null) {
            var succeed = Boolean.TryParse(value, out var typedValue);
            result = typedValue;
            return succeed;
        }
    }

    public class Int32PropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.Int32TypeName;
        public override Type ClrType => typeof(int);

        public override bool TryParsePropertyValue(MetaProperty property, string value, out object result, string format = null) {
            var succeed = Int32.TryParse(value, out var typedValue);
            result = typedValue;
            return succeed;
        }
    }

    public class Int64PropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.Int64TypeName;
        public override Type ClrType => typeof(long);

        public override bool TryParsePropertyValue(MetaProperty property, string value, out object result, string format = null) {
            var succeed = Int64.TryParse(value, out var typedValue);
            result = typedValue;
            return succeed;
        }
    }

    public class FloatPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.FloatTypeName;
        public override Type ClrType => typeof(float);

        public override bool TryParsePropertyValue(MetaProperty property, string value, out object result, string format = null) {
            var succeed = Single.TryParse(value, out var typedValue);
            result = typedValue;
            return succeed;
        }
    }

    public class DoublePropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.DoubleTypeName;
        public override Type ClrType => typeof(double);

        public override bool TryParsePropertyValue(MetaProperty property, string value, out object result, string format = null) {
            var succeed = Double.TryParse(value, out var typedValue);
            result = typedValue;
            return succeed;
        }
    }

    public class StringPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.StringTypeName;
        public override Type ClrType => typeof(string);

        public override bool TryParsePropertyValue(MetaProperty property, string value, out object result, string format = null) {
            result = value;
            return true;
        }
    }

    public class ByteArrayPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.ByteArrayTypeName;
        public override Type ClrType => typeof(byte[]);

        public override bool TryParsePropertyValue(MetaProperty property, string value, out object result, string format = null) {
            throw new NotSupportedException();
        }
    }

    public class DecimalPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.DecimalTypeName;
        public override Type ClrType => typeof(decimal);

        public override bool TryParsePropertyValue(MetaProperty property, string value, out object result, string format = null) {
            var succeed = Decimal.TryParse(value, out var typedValue);
            result = typedValue;
            return succeed;
        }
    }

    public class DateTimePropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.DateTimeTypeName;
        public override Type ClrType => typeof(DateTime);

        public override bool TryParsePropertyValue(MetaProperty property, string value, out object result, string format = null) {
            //TODO
            var succeed = DateTime.TryParse(value, out var typedValue);
            result = typedValue;
            return succeed;
        }
    }

    public class TimeSpanPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.TimeSpanTypeName;
        public override Type ClrType => typeof(TimeSpan);

        public override bool TryParsePropertyValue(MetaProperty property, string value, out object result, string format = null) {
            //TODO
            var succeed = TimeSpan.TryParse(value, out var typedValue);
            result = typedValue;
            return succeed;
        }
    }

}
