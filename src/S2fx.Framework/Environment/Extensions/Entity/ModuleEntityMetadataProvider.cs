using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using OrchardCore.Environment.Extensions;
using OrchardCore.Modules;
using S2fx.Model.Annotations;
using S2fx.Model.Metadata;
using System.ComponentModel;

namespace S2fx.Environment.Extensions.Entity {

    public class ModuleEntityMetadataProvider : IEntityMetadataProvider {

        private readonly IHostingEnvironment _environment;

        public ModuleEntityMetadataProvider(IHostingEnvironment environment) {
            _environment = environment;
        }

        public IEnumerable<EntityInfo> GetEntitiesMetadata(string moduleName) {

            var assembly = _environment.GetModule(moduleName).Assembly;
            var entityTypes = assembly.ExportedTypes
                .Where(t => t.IsClass | t.IsPublic && t.GetCustomAttributes<EntityAttribute>().Count() > 0);

            foreach (var et in entityTypes) {
                var entityAttribute = et.GetCustomAttribute<EntityAttribute>() ?? throw new InvalidOperationException();
                var displayName = et.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? entityAttribute.Name;

                var descriptor = new EntityInfo() {
                    Name = entityAttribute.Name,
                    DisplayName = displayName,
                    Type = et,
                    Attributes = et.GetCustomAttributes()
                };

                yield return descriptor;
            }
        }
    }

}
