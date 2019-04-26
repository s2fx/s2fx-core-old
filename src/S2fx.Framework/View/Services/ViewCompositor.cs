using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.View.Schemas;

namespace S2fx.View.Services {

    public class ViewCompositor : IViewCompositor {
        public Task<AbstractEntityViewDefinition> ComposeViewAsync(string viewName) {
            throw new NotImplementedException();
        }
    }

}
