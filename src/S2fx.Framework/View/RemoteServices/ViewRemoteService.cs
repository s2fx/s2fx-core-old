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

        public ViewRemoteService(IMenuService menuService) {
            _menuService = menuService;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: false)]
        public virtual async Task<IEnumerable<MenuItem>> MainMenu() {
            return await _menuService.GetMainMenuTreeAsync();
        }

    }

}
