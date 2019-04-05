using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Remoting {

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RemoteServiceMethodAttribute : Attribute {

        public string Name { get; set; } = null;

        public HttpMethod HttpMethod { get; }
        public bool IsRestful { get; }

        public RemoteServiceMethodAttribute(HttpMethod httpMethod = HttpMethod.Post, bool isRestful = false) {
            this.HttpMethod = httpMethod;
            this.IsRestful = isRestful;
        }
    }

}
