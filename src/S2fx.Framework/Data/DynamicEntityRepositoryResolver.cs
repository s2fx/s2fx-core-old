using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

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

    public class DynamicEntityRepositoryResolver : IDynamicEntityRepositoryResolver {
        private readonly IServiceProvider _services;

        public DynamicEntityRepositoryResolver(IServiceProvider services) {
            _services = services;
        }

        public IRepository ResolveRepository(Type entityType) {
            if (entityType == null) {
                throw new ArgumentNullException(nameof(entityType));
            }

            //TODO cache
            var repositoryType = typeof(IRepository<>).MakeGenericType(entityType);
            return (IRepository)_services.GetService(repositoryType);
        }
    }

}
