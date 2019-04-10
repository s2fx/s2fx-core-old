using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Conventions;
using S2fx.Data.Importing;
using S2fx.Data.Seeding;
using S2fx.Data.UnitOfWork;

namespace S2fx.Data {

    public static class ServiceCollectionExtensions {

        public static void AddS2fxData(this IServiceCollection services) {

            // services.AddTransient<IDynamicEntityRepositoryResolver, DynamicEntityRepositoryResolver>();

            services.AddScoped<IUnitOfWorkManager, DefaultUnitOfWorkManager>();
            services.AddSingleton<IDbNameConvention, S2DbNameConvention>();

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
