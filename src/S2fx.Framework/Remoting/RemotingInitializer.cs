using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrchardCore.Modules;

namespace S2fx.Remoting {

    public class RemotingInitializer : ModularTenantEvents {
        private readonly ILogger<RemotingInitializer> _logger;
        private readonly IServiceProvider _serviceProvider;

        public RemotingInitializer(ILogger<RemotingInitializer> logger, IServiceProvider sp) {
            _logger = logger;
            _serviceProvider = sp;
        }

        public override async Task ActivatingAsync() {
            // 此类不要直接从构造函数注入需要的接口

            _logger.LogInformation("A tenant has been activated.");
            var rsm = _serviceProvider.GetRequiredService<IRemoteServiceManager>();
            await rsm.InitializeAsync();
        }

    }

}
