using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S2fx.Data;
using S2fx.Model;
using S2fx.Remoting;
using S2fx.View.Model;
using S2fx.View.Model.Model;
using Schemas = S2fx.View.Schemas;
using S2fx.View.Services;
using S2fx.Model.Metadata;

namespace S2fx.View.RemoteServices {

    [RemoteService(name: "View", Area = MvcControllerAreas.MetadataArea)]
    public class ViewRemoteService {
        readonly IMenuService _menuService;
        readonly IViewManager _viewManager;
        readonly IEntityManager _entityManager;

        public ViewRemoteService(IMenuService menuService, IViewManager viewManager, IEntityManager entityManager) {
            _menuService = menuService;
            _viewManager = viewManager;
            _entityManager = entityManager;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: false)]
        public virtual async Task<IEnumerable<MenuItem>> MainMenu() {
            return await _menuService.GetMainMenuTreeAsync();
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: true)]
        public virtual async Task<ViewInfo> SingleView([Url]string name) {
            var composedView = await _viewManager.GetComposedViewAsync(name);
            var metaEntity = _entityManager.GetEntity(composedView.Entity);

            IEnumerable<MetaField> fields = null;
            if (composedView is Schemas.ListView listView) {
                fields = listView.Columns
                    .OfType<Schemas.Field>()
                    .Select(x => metaEntity.Fields[x.Name]);
            }
            return new ViewInfo(name, composedView, fields);

        }
    }

}
