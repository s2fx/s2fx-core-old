using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using S2fx.Metadata.Services;

namespace S2fx.Core {

    public class Startup : StartupBase {

        public override void ConfigureServices(IServiceCollection services) {
            /*
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<IDisplayDriver<ISite>, SmtpSettingsDisplayDriver>();
            services.AddScoped<INavigationProvider, AdminMenu>();

            services.AddScoped<IConfigureOptions<SmtpSettings>, SmtpSettingsConfiguration>();
            services.AddScoped<ISmtpService, SmtpService>();
            */
            services.AddS2fx();

            services.AddTransient<ITestService, TestService>();
        }
    }
}