using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using S2fx.Data.Importing;
using S2fx.Metadata.Services;
using S2fx.Modules;
using S2fx.View;

namespace S2fx.Core {

    public class Startup : S2StartupBase {

        public override void ConfigureServices(IServiceCollection services) {
            services.AddTransient<ITestService, TestService>();
        }

        public override void ConfigureSeeds(ISeedManifestCollection initSeeds, ISeedManifestCollection demoSeeds) {
            initSeeds.AddManifestFile("SeedData/Init/Manifest.xaml");
            
            demoSeeds.AddManifestFile("SeedData/demo/Manifest.xaml");
        }

        public override void ConfigureViews(IViewDefinitionsCollection views) {
            views.AddViewFile("S2Views/Menus.xaml");
            views.AddViewFile("S2Views/UserViews.xaml");
            views.AddViewFile("S2Views/RoleViews.xaml");
            views.AddViewFile("S2Views/PermissionViews.xaml");
            views.AddViewFile("S2Views/ModuleViews.xaml");
            views.AddViewFile("S2Views/SequenceViews.xaml");
            views.AddViewFile("S2Views/ViewViews.xaml");
        }

    }
}