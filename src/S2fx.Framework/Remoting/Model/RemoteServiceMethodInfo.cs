using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace S2fx.Remoting.Model {

    public class RemoteServiceMethodInfo {
        public string Name { get; set; }
        public MethodInfo ClrMethodInfo { get; set; }
    }

}
