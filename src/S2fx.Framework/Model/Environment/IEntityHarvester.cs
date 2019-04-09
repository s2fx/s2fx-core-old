using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Model.Environment {

    public interface IEntityHarvester {
        int Priority { get; }
        Task<IEnumerable<EntityEntry>> HarvestEntitiesAsync();

    }

}
