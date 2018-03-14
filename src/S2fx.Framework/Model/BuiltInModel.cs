using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;

namespace S2fx.Model {
    public static class BuiltInModel {
        private readonly static Type[] s_builtInEntityTypes = new Type[] {
            typeof(MetaModuleEntity)
        };

        public static IEnumerable<Type> BuiltInEntityTypes => s_builtInEntityTypes;
    }
}
