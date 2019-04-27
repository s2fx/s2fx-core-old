using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Data;
using S2fx.View.Model.Model;
using S2fx.View.Schemas;
using S2fx.Xaml;

namespace S2fx.View.Services {

    public class ViewManager : IViewManager {
        readonly IHttpContextAccessor _httpContextAccessor;

        public ViewManager(IHttpContextAccessor hca) {
            _httpContextAccessor = hca;
        }

        public async Task<AbstractEntityViewDefinition> GetComposedViewAsync(string viewName) {
            var serviceProvider = _httpContextAccessor.HttpContext.RequestServices;
            var viewRepo = serviceProvider.GetRequiredService<ISafeRepository<ViewEntity>>();
            var xaml = serviceProvider.GetRequiredService<IXamlService>();
            var view = await viewRepo.SingleAsync(x => x.Name == viewName);
            return await xaml.LoadFromStringAsync<AbstractEntityViewDefinition>(view.Definition);
        }

    }

}
