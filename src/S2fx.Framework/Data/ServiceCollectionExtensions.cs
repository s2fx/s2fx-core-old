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

            services.AddTransient<IUnitOfWorkManager, DefaultUnitOfWorkManager>();
            services.AddTransient<IDbNameConvention, S2DbNameConvention>();

            //seeding
            services.AddTransient<ISeedHarvester, FileSystemSeedHarvester>();
            services.AddTransient<ISeedLoader, SeedDataLoader>();

            services.AddTransient(typeof(GenericRecordImporter<>));
            services.AddTransient(typeof(GenericRecordFinder<>));

            services.AddTransient<IDataImporter, DataImporter>();

            services.AddTransient<IDataSource, XmlDataSource>();
            services.AddTransient<IDataSource, CsvDataSource>();
        }

    }

}
