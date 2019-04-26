using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using S2fx.View.Model.Model;
using S2fx.View.Schemas;

namespace S2fx.View.Services {

    /// <summary>
    /// 视图合成器
    /// </summary>
    public interface IViewCompositor {

        Task<AbstractEntityViewDefinition> ComposeViewAsync(string viewName);

    }

}
