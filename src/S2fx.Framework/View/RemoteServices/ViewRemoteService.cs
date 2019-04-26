using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S2fx.Data;
using S2fx.Remoting;
using S2fx.View.Model;
using S2fx.View.Model.Model;

namespace S2fx.View.RemoteServices {

    [RemoteService(name: "View", Area = MvcControllerAreas.MetadataArea)]
    public class ViewRemoteService {
        readonly ISafeRepository<MenuItemEntity> _menuRepo;

        public ViewRemoteService(ISafeRepository<MenuItemEntity> menuRepo) {
            _menuRepo = menuRepo;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: false)]
        public virtual async Task<IEnumerable<MenuItem>> MainMenu() {
            var menus = await _menuRepo.GetAllAsync();
            var parents = menus.ToLookup(x => x._Parent?.Id);
            Func<long?, IEnumerable<MenuItem>> buildTree = null;
            buildTree = pid => parents[pid]
                     .OrderBy(x => x.Order)
                     .Select(x => new MenuItem(x.Id, x.Name, x.Text, x.Order, x._Parent?.Id, buildTree(x.Id), x.Action?.Id, x.Action?.Name, x.Icon, x.BackgroundColor));

            return buildTree(null);
        }

    }

}
