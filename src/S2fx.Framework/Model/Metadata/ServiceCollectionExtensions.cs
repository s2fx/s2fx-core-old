using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Model.Metadata.Conventions;
using S2fx.Model.Metadata.Types;

namespace S2fx.Model.Metadata {

    public static class ServiceCollectionExtensions {

        public static void RegisterAllPropertyTypes(this IServiceCollection services) {

            var assembly = Assembly.GetExecutingAssembly();
            var propertyTypes = assembly.ExportedTypes
                .Where(t => t.IsPublic && t.IsClass && !t.IsAbstract && typeof(IPropertyType).IsAssignableFrom(t));
            foreach (var pt in propertyTypes) {
                services.AddSingleton(typeof(IPropertyType), pt);
            }
        }

        public static void RegisterAllEntityTypes(this IServiceCollection services) {

            var assembly = Assembly.GetExecutingAssembly();
            var entityTypes = assembly.ExportedTypes
                .Where(t => t.IsPublic && t.IsClass && !t.IsAbstract && typeof(IEntityType).IsAssignableFrom(t));
            foreach (var pt in entityTypes) {
                services.AddSingleton(typeof(IEntityType), pt);
            }
        }

        public static void RegisterBuiltinMetadataConventions(this IServiceCollection services) {

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
