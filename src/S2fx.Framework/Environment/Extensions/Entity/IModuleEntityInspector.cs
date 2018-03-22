using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.Environment.Extensions;
using OrchardCore.Modules;

namespace S2fx.Environment.Extensions.Entity {

    public interface IModuleEntityInspector {

        Task<IEnumerable<EntityDescriptor>> InspectEntitiesAsync(Module module);

    }

}
