using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using S2fx.Model.Metadata;
using S2fx.Model.Entities;

namespace S2fx.Model {

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
