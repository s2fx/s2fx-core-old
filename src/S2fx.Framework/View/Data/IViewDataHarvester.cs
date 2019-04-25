using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.Data.Importing.Model;
using S2fx.View.Schemas;

namespace S2fx.View.Data {

    public interface IViewDataHarvester {
        Task<IEnumerable<IViewDefinition>> HarvestViewDefinitionsAsync();
    }

}
