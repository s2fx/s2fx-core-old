using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Remoting {

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class RemoteServiceAttribute : Attribute {
        public string Name { get; set; } = null;
    }

}
