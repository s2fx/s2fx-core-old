using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using S2fx.Model.Annotations;
using S2fx.Model.Metadata;
using System.ComponentModel;
using S2fx.Environment.Extensions.Entity;
using S2fx.Model.Metadata.Loaders;

namespace S2fx.Model {

    public class BuiltInEntityMetadataProvider : IEntityMetadataProvider {
        private readonly IClrTypeEntityMetadataLoader _loader;

        public BuiltInEntityMetadataProvider(IClrTypeEntityMetadataLoader loader) {
            _loader = loader;
        }

        public IEnumerable<EntityInfo> GetEntitiesMetadata(string moduleName) {

            var entityTypes = BuiltInModel.BuiltInEntityTypes;

            foreach (var et in entityTypes) {
                yield return _loader.LoadClrType(et);
            }
        }
    }

}
