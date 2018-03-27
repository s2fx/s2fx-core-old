using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace S2fx.Model.Metadata.Types {

    public class BooleanPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.BooleanTypeName;
        public override Type ClrType => typeof(bool);

        public override bool TryParsePropertyValue(string s, out object value) {
            var result = bool.TryParse(s, out var typedValue);
            value = typedValue;
            return result;
        }
    }

    public class Int32PropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.Int32TypeName;
        public override Type ClrType => typeof(int);

        public override bool TryParsePropertyValue(string s, out object value) {
            var result = int.TryParse(s, out var typedValue);
            value = typedValue;
            return result;
        }
    }

    public class Int64PropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.Int64TypeName;
        public override Type ClrType => typeof(long);

        public override bool TryParsePropertyValue(string s, out object value) {
            var result = long.TryParse(s, out var typedValue);
            value = typedValue;
            return result;
        }
    }

    public class FloatPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.FloatTypeName;
        public override Type ClrType => typeof(float);

        public override bool TryParsePropertyValue(string s, out object value) {
            var result = float.TryParse(s, out var typedValue);
            value = typedValue;
            return result;
        }
    }

    public class DoublePropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.DoubleTypeName;
        public override Type ClrType => typeof(double);

        public override bool TryParsePropertyValue(string s, out object value) {
            var result = double.TryParse(s, out var typedValue);
            value = typedValue;
            return result;
        }
    }

    public class StringPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.StringTypeName;
        public override Type ClrType => typeof(string);

        public override bool TryParsePropertyValue(string s, out object value) {
            value = s;
            return true;
        }
    }

    public class ByteArrayPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.ByteArrayTypeName;
        public override Type ClrType => typeof(byte[]);

        public override bool TryParsePropertyValue(string s, out object value) {
            throw new NotSupportedException();
        }
    }

    public class DecimalPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.DecimalTypeName;
        public override Type ClrType => typeof(decimal);

        public override bool TryParsePropertyValue(string s, out object value) {
            var result = decimal.TryParse(s, out var typedValue);
            value = typedValue;
            return result;
        }
    }

    public class DateTimePropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.DateTimeTypeName;
        public override Type ClrType => typeof(DateTime);

        public override bool TryParsePropertyValue(string s, out object value) {
            var result = DateTime.TryParse(s, out var typedValue);
            value = typedValue;
            return result;
        }
    }

    public class TimeSpanPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.TimeSpanTypeName;
        public override Type ClrType => typeof(TimeSpan);

        public override bool TryParsePropertyValue(string s, out object value) {
            var result = TimeSpan.TryParse(s, out var typedValue);
            value = typedValue;
            return result;
        }
    }

}
