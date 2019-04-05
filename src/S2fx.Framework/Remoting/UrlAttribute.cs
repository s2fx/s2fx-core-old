using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Remoting {

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = false)]
    public class UrlAttribute : Attribute {
    }

}
