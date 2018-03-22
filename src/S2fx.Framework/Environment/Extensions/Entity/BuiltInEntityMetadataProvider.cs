using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using S2fx.Model.Annotations;
using System.ComponentModel;
using S2fx.Environment.Extensions.Entity;

namespace S2fx.Environment.Extensions.Entity {

    /*
    public class BuiltInEntityMetadataProvider : IModuleEntityInspector {
        private readonly IClrTypeEntityMetadataLoader _loader;

        public BuiltInEntityMetadataProvider(IClrTypeEntityMetadataLoader loader) {
            _loader = loader;
        }

        public IEnumerable<MetaEntity> ScanEntitiesInModule(string moduleName) {

            var assembly = Assembly.GetExecutingAssembly();
            var entityTypes = assembly.ExportedTypes
                .Where(t => t.GetCustomAttribute<EntityAttribute>() != null);
            //TODO 拓扑排序
            var loadedEntities = new Dictionary<string, MetaEntity>(entityTypes.Count());
            var context = new MetadataContext(loadedEntities);

            foreach (var et in entityTypes) {
                var entity = _loader.LoadEntityByClr(context, et);
                loadedEntities.Add(entity.Name, entity);
                yield return entity;
            }
        }
    }
    */

}
