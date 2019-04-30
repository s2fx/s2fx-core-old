using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using S2fx.Modules;
using S2fx.View;

namespace S2fx.SampleModule {

    /// <summary>
    /// The module startup
    /// </summary>
    public class Startup : S2StartupBase {

        public override void ConfigureServices(IServiceCollection services) {
        }

        public override void ConfigureViews(IViewDefinitionsCollection views) {
            views.AddViewFile("S2Views/Menus.xaml");
        }

    }
}