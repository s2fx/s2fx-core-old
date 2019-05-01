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

    [RemoteService(name: "Action", Area = MvcControllerAreas.MetadataArea)]
    public class ActionRemoteService {
        readonly IServiceProvider _serviceProvider;

        public ActionRemoteService(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: true)]
        public virtual async Task<ActionInfo> Single([Url]long id) {
            var actionRepo = _serviceProvider.GetRequiredService<ISafeRepository<ActionEntity>>().Sudo();
            var record = await actionRepo.SingleAsync(id);
            var xaml = _serviceProvider.GetRequiredService<IXamlService>();
            var definition = await xaml.LoadFromStringAsync<Schemas.AbstractActionDefinition>(record.Definition);
            return new ActionInfo(record.Id, record.ActionType, record.CanBeHome, record.Priority, definition);
        }

    }

}
