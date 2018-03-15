using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using S2fx.Model.Annotations;
using System.ComponentModel;

namespace S2fx.Model.Metadata.Loaders {

    public class ClrTypeEntityMetadataLoader : IClrTypeEntityMetadataLoader {

        public EntityInfo LoadClrType(Type entityType) {

            var entityAttribute = entityType.GetCustomAttribute<EntityAttribute>() ?? throw new InvalidOperationException();
            var displayName = entityType.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? entityAttribute.Name;

            return new EntityInfo() {
                Name = entityAttribute.Name,
                DisplayName = displayName,
                ClrType = entityType,
                Attributes = entityType.GetCustomAttributes()
            };

        }

    }

}
