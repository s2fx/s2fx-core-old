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
using Microsoft.Extensions.DependencyInjection;
using S2fx.Xaml;

namespace S2fx.View.RemoteServices {

    [RemoteService(name: "View", Area = MvcControllerAreas.MetadataArea)]
    public class ViewRemoteService {
        readonly IServiceProvider _serviceProvider;
        public ViewRemoteService(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: false)]
        public virtual async Task<IEnumerable<MenuItem>> MainMenu() {
            var menuService = _serviceProvider.GetRequiredService<IMenuService>();
            return await menuService.GetMainMenuTreeAsync();
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: true)]
        public virtual async Task<ViewInfo> SingleView([Url]string name) {
            var viewManager = _serviceProvider.GetRequiredService<IViewManager>();
            var entityManager = _serviceProvider.GetRequiredService<IEntityManager>();
            var composedView = await viewManager.GetComposedViewAsync(name);
            var metaEntity = entityManager.GetEntity(composedView.Entity);

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
