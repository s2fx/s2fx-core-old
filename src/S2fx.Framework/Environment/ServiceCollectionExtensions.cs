using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Descriptor;
using S2fx.Convention;
using S2fx.Data.Seeding;
using S2fx.Data.UnitOfWork;
using S2fx.Environment.Extensions;
using S2fx.Environment.Extensions.Entity;
using S2fx.Environment.Shell;
using S2fx.Remoting;

namespace S2fx.Environment {

    public static class ServiceCollectionExtensions {

        public static void AddS2Environment(this IServiceCollection services) {
            services.AddTransient<IEntityHarvester, BuiltinEntityHarvester>();
            services.AddTransient<IEntityHarvester, ClrEntityHarvester>();

            services.AddSingleton<IS2ModuleManager, S2ModuleManager>();
            services.Replace(new ServiceDescriptor(typeof(IShellDescriptorManager), typeof(S2ShellDescriptorManager), ServiceLifetime.Scoped));
            services.Replace(new ServiceDescriptor(typeof(IShellStateManager), typeof(S2ShellStateManager), ServiceLifetime.Scoped));

            services.AddTransient<IShellFeatureService, ShellFeatureService>();
        }

    }

}
