using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using OrchardCore.Modules;
using S2fx.Model.Metadata;
using S2fx.Model;
using S2fx.Model.Annotations;

namespace S2fx.Environment.Extensions {

    public static class ModuleExtensions {
        public static IEnumerable<Type> GetEntityTypes(this Module module) {
            return module.Assembly
                .ExportedTypes
                .Where(t => t.IsClass && t.CustomAttributes.Any(a => a.AttributeType == typeof(TableAttribute)));
        }

    }
}
