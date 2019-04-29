using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Conventions;
using S2fx.View.Data;
using S2fx.View.Services;

namespace S2fx.View {

    internal static class ServiceCollectionExtensions {

        internal static void AddS2fxViewGlobal(this IServiceCollection services) {
        }

        internal static void AddS2fxViewTenant(this IServiceCollection services) {
            services.AddTransient<IViewHarvester, S2StartupViewHarvester>();
            services.AddScoped<IViewDataSynchronizer, ViewDataSynchronizer>();

            services.AddTransient<IViewCompositor, ViewCompositor>();

            services.AddScoped<IMenuService, MenuService>();

            services.AddSingleton<IActionManager, ActionManager>();
            services.AddSingleton<IViewManager, ViewManager>();
        }
    }

}
