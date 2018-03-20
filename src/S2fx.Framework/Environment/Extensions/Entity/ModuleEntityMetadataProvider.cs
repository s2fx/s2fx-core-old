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

        public IEnumerable<MetaEntity> GetEntitiesMetadata(string moduleName) {

            var assembly = _environment.GetModule(moduleName).Assembly;
            var entityTypes = assembly.ExportedTypes
                .Where(t => t.GetCustomAttribute<EntityAttribute>() != null);

            foreach (var et in entityTypes) {
                if (!et.IsClass || !et.IsPublic || et.IsAbstract) {
                    throw new InvalidOperationException($"The entity `{et.FullName}` must be a non-abstract public class and must have the Entity attribute");
                }
                yield return _clrEntityLoader.LoadEntityByClr(et);
            }
        }
    }

}
