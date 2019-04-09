using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using S2fx.Model.Metadata.Conventions;
using S2fx.Model.Metadata.Types;

namespace S2fx.Model.Metadata {

    public static class ServiceCollectionExtensions {

        public static void RegisterAllBuiltinEntityFieldTypes(this IServiceCollection services) {

            var assembly = Assembly.GetExecutingAssembly();
            var propertyTypes = assembly.ExportedTypes
                .Where(t => t.IsPublic && t.IsClass && !t.IsAbstract && typeof(IFieldType).IsAssignableFrom(t));
            foreach (var pt in propertyTypes) {
                services.AddSingleton(typeof(IFieldType), pt);
            }
        }

        public static void RegisterAllBuiltinEntityTypes(this IServiceCollection services) {

            var assembly = Assembly.GetExecutingAssembly();
            var entityTypes = assembly.ExportedTypes
                .Where(t => t.IsPublic && t.IsClass && !t.IsAbstract && typeof(IEntityType).IsAssignableFrom(t));
            foreach (var pt in entityTypes) {
                services.AddSingleton(typeof(IEntityType), pt);
            }
        }

        public static void RegisterAllBuiltinEntityMetadataConventions(this IServiceCollection services) {

            var assembly = Assembly.GetExecutingAssembly();
            var conventionTypes = assembly.ExportedTypes
                .Where(t => t.IsPublic && t.IsClass && !t.IsAbstract && typeof(IMetadataConvention).IsAssignableFrom(t));

            foreach (var ct in conventionTypes) {
                var interfaceType = ct.GetInterfaces().Single(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IMetadataConvention<>));
                services.AddTransient(interfaceType, ct);
            }
        }
    }

}
