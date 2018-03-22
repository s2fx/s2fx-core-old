using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using S2fx.Model.Metadata;

namespace S2fx.Model.Builtin {

    public static class BuiltInModel {

        private readonly static Type[] s_builtInEntityTypes = new Type[] {
            typeof(UserEntity),
            typeof(ModuleEntity),
            typeof(SelectionEntity),
            typeof(SelectionItemEntity),
            typeof(SequenceEntity),
            typeof(RoleEntity),
        };

        public static IEnumerable<Type> BuiltInEntityTypes => s_builtInEntityTypes;
    }
}
