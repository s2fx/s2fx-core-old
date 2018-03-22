using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Environment.Extensions.Entity {

    public interface IEntityHarvester {

        Task<IEnumerable<EntityDescriptor>> HarvestEntitiesAsync();

    }

}
