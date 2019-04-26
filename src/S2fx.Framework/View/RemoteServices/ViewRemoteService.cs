using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S2fx.Data;
using S2fx.Remoting;
using S2fx.View.Model;
using S2fx.View.Model.Model;
using S2fx.View.Services;

namespace S2fx.View.RemoteServices {

    [RemoteService(name: "View", Area = MvcControllerAreas.MetadataArea)]
    public class ViewRemoteService {
        readonly IMenuService _menuService;
        readonly IViewManager _viewManager;

        public ViewRemoteService(IMenuService menuService, IViewManager viewManager) {
            _menuService = menuService;
            _viewManager = viewManager;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: false)]
        public virtual async Task<IEnumerable<MenuItem>> MainMenu() {
            return await _menuService.GetMainMenuTreeAsync();
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: true)]
        public virtual async Task<ViewInfo> SingleView([Url]string name) {
            var composedView = await _viewManager.GetComposedViewAsync(name);
            return new ViewInfo(name, null, composedView, null);
        }
    }

}
