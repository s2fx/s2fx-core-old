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

namespace S2fx.Model {

    public class BuiltInEntityMetadataProvider : IEntityMetadataProvider {

        public IEnumerable<EntityInfo> GetEntitiesMetadata(string moduleName) {

            var entityTypes = BuiltInModel.BuiltInEntityTypes;

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
