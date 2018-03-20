using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using S2fx.Data.EFCore.Mapping;
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

            //EFCore stuffs
            services.AddTransient<IEFCoreModelMapper, EFCoreModelMapper>();
            services.AddTransient<IEFCoreMappingStrategy, DefaultEFCoreMappingStrategy>();

            //Unit of work
            services.AddTransient<DbContext, S2DbContext>();
            services.AddTransient<IUnitOfWork, EFCoreUnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(DefaultEFCoreRepository<>));
        }

    }
}