using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Environment.Extensions;
using S2fx.Model.Metadata;

namespace S2fx.Environment.Extensions.Entity {

    public interface IEntityMetadataProvider {

        IEnumerable<MetaEntity> GetEntitiesMetadata(string moduleName);

    }

}
