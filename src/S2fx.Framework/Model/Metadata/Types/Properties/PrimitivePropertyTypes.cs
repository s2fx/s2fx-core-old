using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace S2fx.Model.Metadata.Types {

    public class BooleanPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.BooleanTypeName;
        public override Type ClrType => typeof(bool);
    }

    public class Int32PropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.Int32TypeName;
        public override Type ClrType => typeof(int);
    }

    public class Int64PropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.Int64TypeName;
        public override Type ClrType => typeof(long);
    }

    public class FloatPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.FloatTypeName;
        public override Type ClrType => typeof(float);
    }

    public class DoublePropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.DoubleTypeName;
        public override Type ClrType => typeof(double);
    }

    public class StringPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.StringTypeName;
        public override Type ClrType => typeof(string);
    }

    public class ByteArrayPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.ByteArrayTypeName;
        public override Type ClrType => typeof(byte[]);
    }

    public class DecimalPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.DecimalTypeName;
        public override Type ClrType => typeof(decimal);
    }

    public class DateTimePropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.DateTimeTypeName;
        public override Type ClrType => typeof(DateTime);
    }

    public class TimeSpanPropertyType : AbstractPrimitivePropertyType {
        public override string Name => BuiltinPropertyTypeNames.TimeSpanTypeName;
        public override Type ClrType => typeof(TimeSpan);
    }

}
