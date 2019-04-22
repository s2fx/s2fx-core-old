using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S2fx.Data;
using S2fx.Remoting;
using S2fx.View.Model;
using S2fx.View.Model.Model;

namespace S2fx.View.Services {

    [RemoteService(name: "View", Area = MvcControllerAreas.MetadataArea)]
    public class ViewRemoteService {
        readonly IRepository<MenuItemEntity> _menuRepo;

        public ViewRemoteService(IRepository<MenuItemEntity> menuRepo) {
            _menuRepo = menuRepo;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: true)]
        public virtual async Task<IEnumerable<MenuItem>> GetMainMenuTree() {
            var menus = await _menuRepo.GetAllAsync();
            return menus.Select(x => new MenuItem(x.Id, x.Name, x.Text, null));
        }

    }

}
