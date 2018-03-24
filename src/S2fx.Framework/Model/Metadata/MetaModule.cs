using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Modules;
using S2fx.Environment.Extensions;

namespace S2fx.Model.Metadata {

    public class MetaModule {

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string State { get; set; }

        public Version Version { get; set; }

        public Module Module { get; set; }
    }

}
