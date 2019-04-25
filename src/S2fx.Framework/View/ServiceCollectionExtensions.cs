using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Conventions;
using S2fx.Data.Importing;
using S2fx.Data.Importing.DataSources;
using S2fx.Data.Repositories;
using S2fx.Data.Seeding;
using S2fx.Data.Transactions;

namespace S2fx.View {

    internal static class ServiceCollectionExtensions {

        internal static void AddS2fxViewGlobal(this IServiceCollection services) {
        }

        internal static void AddS2fxViewTenant(this IServiceCollection services) {
        }
    }

}
