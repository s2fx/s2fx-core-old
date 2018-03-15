using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace S2fx.Utility {

    public static class TypeExtensions {

        public static bool IsImplementsInterface(this Type type, Type interfaceType) =>
            type.GetInterfaces().Contains(interfaceType);

        public static bool IsImplementsInterface<T>(this Type type) =>
            type.IsImplementsInterface(typeof(T));



    }

}
