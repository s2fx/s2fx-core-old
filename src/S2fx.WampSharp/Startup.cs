using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;

namespace S2fx.WampSharp {

    public class Startup : StartupBase {

        public override void ConfigureServices(IServiceCollection services) {
            services.AddS2Wamp();
        }

    }
}