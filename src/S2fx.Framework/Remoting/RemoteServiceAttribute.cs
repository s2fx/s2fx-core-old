using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Remoting {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class RemoteServiceAttribute : Attribute {

        public string Name { get; }

        public string Area { get; set; } = string.Empty;

        public RemoteServiceAttribute(string name) {
            this.Name = name;
        }

    }

}
