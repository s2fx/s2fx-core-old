using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using S2fx.Data.Importing;
using S2fx.Modules;
using S2fx.View;

namespace S2fx.Setup {

    /// <summary>
    /// The module startup
    /// </summary>
    public class Startup : S2StartupBase {

        public override void ConfigureServices(IServiceCollection services) {
            services.AddSetupServices();
        }

        public override void ConfigureSeeds(ISeedManifestCollection initSeeds, ISeedManifestCollection demoSeeds) {
        }

        public override void ConfigureViews(IViewDefinitionsCollection views) {
        }

    }
}