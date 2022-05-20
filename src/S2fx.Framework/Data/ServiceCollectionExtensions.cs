using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell.Scope;
using OrchardCore.Modules;
using S2fx.Conventions;
using S2fx.Data.Importing;
using S2fx.Data.Importing.DataSources;
using S2fx.Data.Importing.Seeds;
using S2fx.Data.Repositories;
using S2fx.Data.Transactions;

namespace S2fx.Data {

    public interface ITestFoo {
        Task CommitAsync();
    }

    public class TestFoo : ITestFoo {
        public Task CommitAsync() {
            return Task.CompletedTask;
        }

    }

    internal static class ServiceCollectionExtensions {

        internal static void AddS2DataAccessGlobal(this IServiceCollection services) {
            // services.AddTransient<IDynamicEntityRepositoryResolver, DynamicEntityRepositoryResolver>();
            services.AddSingleton<IDbNameConvention, S2DbNameConvention>();

        }

        internal static void AddS2DataAccessTenant(this IServiceCollection services) {

            services.AddScoped(typeof(ISafeRepository<>), typeof(DefaultSafeRepository<>));

            services.AddScoped<ITransactionManager, TransactionManager>();

            services.AddSingleton<ICurrentTransactionAccessor, DefaultCurrentTransactionAccessor>();

            services.AddScoped(sp => {
                var tf = new TestFoo();
                ShellScope.Current.RegisterBeforeDispose(scope => {
                    return scope.ServiceProvider.GetRequiredService<ITestFoo>().CommitAsync();
                });
                return tf;
            });

            //seeding
            services.AddScoped<ISeedHarvester, S2StartupSeedHarvester>();
            services.AddScoped<ISeedSynchronizer, SeedSynchronizer>();

            services.AddScoped(typeof(GenericRecordImporter<>));
            services.AddScoped(typeof(GenericRecordFinder<>));

            services.AddScoped<IDataImporter, DataImporter>();

            services.AddTransient<IDataSource, XmlDataSource>();
            services.AddTransient<IDataSource, CsvDataSource>();

            services.AddScoped<IModularTenantEvents, AutoDBMigrator>();
        }
    }

}
