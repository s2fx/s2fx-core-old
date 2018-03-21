using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Modules.Manifest;

namespace S2fx.Environment.Extensions {

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class S2ModuleAttribute : ModuleAttribute {

        public string Key { get; }

        public S2ModuleAttribute(string key) {
            this.Key = key;
        }
    }

}
