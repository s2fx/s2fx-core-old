using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.View.Data {

    public interface IViewDataSynchronizer {
        Task SynchronizeViewsAsync(string featureId);
        Task SynchronizeAllViewsAsync();
    }

}
