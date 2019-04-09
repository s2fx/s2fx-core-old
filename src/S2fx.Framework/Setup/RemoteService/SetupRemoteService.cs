using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Data;
using S2fx.Remoting;
using S2fx.Setup.Model;
using S2fx.Setup.Services;

namespace S2fx.Setup.RemoteService {

    [RemoteService(name: "Setup", Area = MvcControllerAreas.SystemArea)]
    public class SetupRemoteService {
        readonly ISetupService _setupService;

        public SetupRemoteService(ISetupService setupService) {
            _setupService = setupService;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Post, isRestful: false)]
        public async Task StartSetupAsync(SetupOptions options) {

            var ctx = new SetupContext {
                AdminPassword = options.AdminPassword,
                DbName = options.DbName,
                EnabledFeatures = options.EnabledFeatures,
                IsDemo = options.IsDemo,
            };

            await _setupService.SetupAsync(ctx);
        }

    }

}
