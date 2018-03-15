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
using S2fx.Model.Metadata.Loaders;

namespace S2fx.Environment.Extensions.Entity {

    public class ModuleEntityMetadataProvider : IEntityMetadataProvider {

        private readonly IHostingEnvironment _environment;
        private readonly IClrTypeEntityMetadataLoader _clrEntityLoader;

        public ModuleEntityMetadataProvider(IHostingEnvironment environment, IClrTypeEntityMetadataLoader clrEntityLoader) {
            _environment = environment;
            _clrEntityLoader = clrEntityLoader;
        }

        public IEnumerable<EntityInfo> GetEntitiesMetadata(string moduleName) {

            var assembly = _environment.GetModule(moduleName).Assembly;
            var entityTypes = assembly.ExportedTypes
                .Where(t => t.IsClass | t.IsPublic && t.GetCustomAttributes<EntityAttribute>().Count() > 0);

            foreach (var et in entityTypes) {
                yield return _clrEntityLoader.LoadClrType(et);
            }
        }
    }

}
