using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using S2fx.Metadata.Services;

namespace S2fx.Core {

    public class Startup : StartupBase {

        public override void ConfigureServices(IServiceCollection services) {
            services.AddSlipStreamFramework();
            services.AddTransient<ITestService, TestService>();
        }
    }
}