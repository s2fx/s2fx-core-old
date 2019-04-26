using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.View.Data {

    public interface IViewDataLoader {
        Task LoadViewsAsync(string featureId);
        Task LoadAllViewsAsync();
    }

}
