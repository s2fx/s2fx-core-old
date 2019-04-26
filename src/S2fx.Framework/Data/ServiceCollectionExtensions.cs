using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Conventions;
using S2fx.Data.Importing;
using S2fx.Data.Importing.DataSources;
using S2fx.Data.Importing.Seeds;
using S2fx.Data.Repositories;
using S2fx.Data.Transactions;

namespace S2fx.Data {

    internal static class ServiceCollectionExtensions {

        internal static void AddS2fxDataAccessGlobal(this IServiceCollection services) {
            // services.AddTransient<IDynamicEntityRepositoryResolver, DynamicEntityRepositoryResolver>();
            services.AddSingleton<IDbNameConvention, S2DbNameConvention>();

        }

        internal static void AddS2fxDataAccessTenant(this IServiceCollection services) {

            services.AddScoped(typeof(ISafeRepository<>), typeof(DefaultSafeRepository<>));

            services.AddScoped<ITransactionManager, TransactionManager>();

            services.AddSingleton<ICurrentTransactionAccessor, DefaultCurrentTransactionAccessor>();

            //seeding
            services.AddScoped<ISeedHarvester, ModularSeedHarvester>();
            services.AddScoped<ISeedLoader, SeedDataLoader>();

            services.AddScoped(typeof(GenericRecordImporter<>));
            services.AddScoped(typeof(GenericRecordFinder<>));

            services.AddScoped<IDataImporter, DataImporter>();

            services.AddTransient<IDataSource, XmlDataSource>();
            services.AddTransient<IDataSource, CsvDataSource>();
        }
    }

}
