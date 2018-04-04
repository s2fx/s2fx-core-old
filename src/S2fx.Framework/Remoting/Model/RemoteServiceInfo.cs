using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using OrchardCore.Environment.Extensions.Features;

namespace S2fx.Remoting.Model {

    public class RemoteServiceInfo {
        public string Area { get; set; }
        public IFeatureInfo Feature { get; set; }
        public string Name { get; set; }
        public TypeInfo ClrType { get; set; }
        public IEnumerable<RemoteServiceMethodInfo> Methods { get; set; }

        public override int GetHashCode() =>
            this.Name.GetHashCode();
    }

}
