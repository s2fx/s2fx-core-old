using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;

namespace S2fx.Data.EFCore.PostgreSQL {

    public class Startup : StartupBase {

        public override void ConfigureServices(IServiceCollection services) {
            /*
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<IDisplayDriver<ISite>, SmtpSettingsDisplayDriver>();
            services.AddScoped<INavigationProvider, AdminMenu>();

            services.AddScoped<IConfigureOptions<SmtpSettings>, SmtpSettingsConfiguration>();
            services.AddScoped<ISmtpService, SmtpService>();
            */

            //Unit of work
            services.AddEntityFrameworkNpgsql();
            services.AddTransient<IDbContextOptionsProvider, NpgsqlDbContextOptionsProvider>();
        }

    }
}