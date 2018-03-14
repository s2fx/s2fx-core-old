using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using S2fx.Data.EFCore.UnitOfWork;
using S2fx.Data.UnitOfWork;

namespace S2fx.Data.EFCore {

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
            services.AddTransient<DbContext, S2DbContext>();
            services.AddTransient<IUnitOfWork, EFCoreUnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(DefaultEFCoreRepository<>));
        }

    }
}