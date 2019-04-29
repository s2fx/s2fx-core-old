using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.View.Model;
using S2fx.Data;
using S2fx.Model.Metadata;
using S2fx.View.Model.Model;

namespace S2fx.View.Services {

    public interface IMenuService {
        Task<IEnumerable<MenuItem>> GetMainMenuTreeAsync();
    }


    public class MenuService : IMenuService {
        private readonly ISafeRepository<MenuItemEntity> _menuItemRepo;

        public MenuService(ISafeRepository<MenuItemEntity> menuItemRepo) {
            _menuItemRepo = menuItemRepo;
        }

        public async Task<IEnumerable<MenuItem>> GetMainMenuTreeAsync() {
            var menus = await _menuItemRepo.GetAllAsync();
            var parents = menus.ToLookup(x => x._Parent?.Id);
            Func<long?, int, IEnumerable<MenuItem>> buildTree = null;
            buildTree = (pid, depth) => {
                return parents[pid]
                    .OrderBy(x => x.Order)
                    .Select(x =>
                        new MenuItem(x.Id, x.Name, x.Text, x.Order, x._Parent?.Id, buildTree(x.Id, depth + 1), x.Action?.Id, x.Action?.Name, depth, x.Icon, x.BackgroundColor)
                    );
            };

            return buildTree(null, 0);
        }
    }
}
