using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data {

    public interface IDynamicEntityRepositoryResolver {

        IRepository ResolveRepository(Type entityType);
    }

    public static class DynamicEntityRepositoryResolverExtensions {

        public static IRepository ResolveRepository(this IDynamicEntityRepositoryResolver resolver, string entityTypeFullName) {
            var entityType = Type.GetType(entityTypeFullName);
            return resolver.ResolveRepository(entityType);
        }

    }

}
