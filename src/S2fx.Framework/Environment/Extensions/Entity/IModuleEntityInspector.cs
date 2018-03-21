using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.Environment.Extensions;
using S2fx.Model.Metadata;

namespace S2fx.Environment.Extensions.Entity {

    public interface IModuleEntityInspector {

        Task<IEnumerable<EntityDescriptor>> InspectEntitiesAsync(string moduleId);

    }

}
