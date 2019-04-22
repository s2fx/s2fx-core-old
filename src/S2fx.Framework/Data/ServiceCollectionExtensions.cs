using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Conventions;
using S2fx.Data.Importing;
using S2fx.Data.Importing.DataSources;
using S2fx.Data.Seeding;
using S2fx.Data.UnitOfWork;

namespace S2fx.Data {

    internal static class ServiceCollectionExtensions {

        internal static void AddS2fxDataGlobal(this IServiceCollection services) {
            // services.AddTransient<IDynamicEntityRepositoryResolver, DynamicEntityRepositoryResolver>();
            services.AddSingleton<IDbNameConvention, S2DbNameConvention>();

        }

        internal static void AddS2fxDataTenant(this IServiceCollection services) {
            services.AddScoped<IUnitOfWorkManager, DefaultUnitOfWorkManager>();

            //seeding
            services.AddScoped<ISeedHarvester, FileSystemSeedHarvester>();
            services.AddScoped<ISeedLoader, SeedDataLoader>();

            services.AddScoped(typeof(GenericRecordImporter<>));
            services.AddScoped(typeof(GenericRecordFinder<>));

            services.AddScoped<IDataImporter, DataImporter>();

            services.AddTransient<IDataSource, XmlDataSource>();
            services.AddTransient<IDataSource, CsvDataSource>();
        }
    }

}
