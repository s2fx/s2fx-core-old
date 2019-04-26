using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.View.Schemas;

namespace S2fx.View.Services {

    public interface IViewManager {
        Task<AbstractEntityViewDefinition> GetComposedViewAsync(string viewName);
    }

}
