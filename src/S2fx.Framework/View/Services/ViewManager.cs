using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.View.Schemas;

namespace S2fx.View.Services {

    public class ViewManager : IViewManager {

        public Task<AbstractEntityViewDefinition> GetComposedViewAsync(string viewName) {
            throw new NotImplementedException();
        }

    }

}
