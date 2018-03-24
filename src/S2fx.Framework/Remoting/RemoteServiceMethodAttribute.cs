using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Remoting {

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RemoteServiceMethodAttribute : Attribute {

        public string Name { get; set; } = null;

        public HttpMethod HttpMethod { get; set; } = HttpMethod.Unknown;

    }

}
