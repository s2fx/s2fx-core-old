using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using S2fx.Metadata.Services;
using S2fx.Modules;
using S2fx.View;

namespace S2fx.Core {

    public class Startup : S2StartupBase {

        public override void ConfigureServices(IServiceCollection services) {
            services.AddTransient<ITestService, TestService>();
        }

        public override void ConfigureViews(IViewDefinitionsCollection views) {
            views.AddViewDefinitionsFile("S2Views/Menus.xaml");
            views.AddViewDefinitionsFile("S2Views/UserViews.xaml");
            views.AddViewDefinitionsFile("S2Views/RoleViews.xaml");
            views.AddViewDefinitionsFile("S2Views/PermissionViews.xaml");
            views.AddViewDefinitionsFile("S2Views/ModuleViews.xaml");
            views.AddViewDefinitionsFile("S2Views/SequenceViews.xaml");
            views.AddViewDefinitionsFile("S2Views/ViewViews.xaml");
        }
    }
}