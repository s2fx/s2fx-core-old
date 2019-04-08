using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Environment.Shell.Descriptor;
using S2fx.Conventions;
using S2fx.Data.Seeding;
using S2fx.Data.UnitOfWork;
using S2fx.Environment.Extensions;
using S2fx.Environment.Extensions.Entity;
using S2fx.Environment.Shell;
using S2fx.Environment.Shell.Descriptor;
using S2fx.Remoting;

namespace S2fx.Environment {

    public static class ServiceCollectionExtensions {

        public static void AddS2Environment(this IServiceCollection services) {
            services.TryAddEnumerable(new[] {
                ServiceDescriptor.Transient<IEntityHarvester, BuiltinClrEntityHarvester>(),
                ServiceDescriptor.Transient<IEntityHarvester, EnabledFeaturesClrEntityHarvester>()
            });

            services.AddSingleton<IS2ModuleManager, S2ModuleManager>();
            services.AddSingleton<IShellDescriptorManager, S2ShellDescriptorManager>();
            services.AddScoped<IShellStateManager, S2ShellStateManager>();

            //services.AddScoped<IShellFeaturesManager, ShellFeaturesManager>();
            //services.AddScoped<IShellDescriptorFeaturesManager, ShellDescriptorFeaturesManager>();

            services.AddTransient<IShellFeatureEntityService, ShellFeatureEntityService>();

            services.AddSitesFolder();
        }


        public static void AddSitesFolder(this IServiceCollection services) {
            services.AddSingleton<IShellsSettingsSources, ShellsSettingsSources>();
            services.AddSingleton<IShellsConfigurationSources, ShellsConfigurationSources>();
            services.AddSingleton<IShellConfigurationSources, ShellConfigurationSources>();
            services.AddSingleton<IShellSettingsManager, ShellSettingsManager>();

            services.TryAddEnumerable(new[] {
                ServiceDescriptor.Transient<IConfigureOptions<ShellOptions>, ShellOptionsSetup>(),
            });

        }
    }

}
