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
using S2fx.Environment.Shell;
using S2fx.Environment.Shell.Descriptor;
using S2fx.Remoting;

namespace S2fx.Environment {

    internal static class ServiceCollectionExtensions {

        internal static void AddS2EnvironmentGlobal(this IServiceCollection services) {
            services.AddSitesFolder();
        }

        internal static void AddS2EnvironmentTenant(this IServiceCollection services) {
            services.AddShellDataStorage();
            services.AddTransient<IShellFeatureEntityService, ShellFeatureEntityService>();
        }

        static void AddShellDataStorage(this IServiceCollection services) {
            services.Replace(ServiceDescriptor.Scoped<IShellDescriptorManager, S2ShellDescriptorManager>());
            services.Replace(ServiceDescriptor.Scoped<IShellStateManager, S2ShellStateManager>());
            services.AddScoped<IShellFeaturesManager, ShellFeaturesManager>();
            services.AddScoped<IShellDescriptorFeaturesManager, ShellDescriptorFeaturesManager>();
        }

        static void AddSitesFolder(this IServiceCollection services) {

            services.AddSingleton<IShellsSettingsSources, ShellsSettingsSources>();
            services.AddSingleton<IShellsConfigurationSources, ShellsConfigurationSources>();
            services.AddSingleton<IShellConfigurationSources, ShellConfigurationSources>();
            services.AddTransient<IConfigureOptions<ShellOptions>, ShellOptionsSetup>();
            services.AddSingleton<IShellSettingsManager, ShellSettingsManager>();
        }
    }

}
