using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using NH = NHibernate;
using NHibernate.Cfg;
using OrchardCore.Modules;
using S2fx.Data;
using S2fx.Data.NHibernate;
using S2fx.Data.NHibernate.DbProviders;
using S2fx.Data.NHibernate.Interceptors;
using S2fx.Data.NHibernate.Mapping;
using S2fx.Data.NHibernate.Mapping.Fields;
using S2fx.Data.NHibernate.Transactions;
using S2fx.Data.Transactions;
using S2fx.Utility;

namespace Microsoft.Extensions.DependencyInjection {

    public static class ServiceCollectionExtensions {

        public static void WithNHibernate(this IServiceCollection services) {

            //Unit of work
            //NH stuffs

            //Unit of work
            services.AddScoped<ITransactionFactory, NHTransactionFactory>();

            services.AddScoped(typeof(IRepository<>), typeof(DefaultHibernateRepository<>));

            services.TryAddSingleton<IHibernateDbProviderAccessor, HibernateDbProviderAccessor>();
            services.AddSingleton<INHSessionAccessor, NHTransactionalSessionAccessor>();

            //entity mapping
            services.AddTransient(typeof(EntityMappingClass<>), typeof(EntityMappingClass<>));
            services.AddTransient<IModelMapper, ModelMapper>();
            AddBuiltinFieldMappers(services);

            //interceptors
            services.AddSingleton<NH.IInterceptor, S2NHInterceptor>();

            //Register nhibernate ISessionFactory 
            services.AddTransient<IHibernateConfigurationFactory, HibernateConfigurationFactory>();

            //Register NH's Configuration to container
            services.AddSingletonLazy(sp => sp.GetRequiredService<IHibernateConfigurationFactory>().Create());

            //Register NH's ISessionFactory 
            // TODO 测试多租户
            services.AddSingletonLazy(sp => sp.GetRequiredService<NHibernate.Cfg.Configuration>().BuildSessionFactory());

            //migrator
            services.AddScoped<IDbMigrator, HibernateDbMigrator>();

            services.AddScoped(sp => sp.GetRequiredService<NH.ISessionFactory>().OpenSession());
        }

        private static void AddBuiltinFieldMappers(IServiceCollection services) {
            var assembly = Assembly.GetExecutingAssembly();
            var mapperTypes = assembly.ExportedTypes.Where(t => t.IsPublic && t.IsClass && !t.IsAbstract && t.IsImplementsInterface<IFieldMapper>());
            foreach (var mt in mapperTypes) {
                services.AddTransient(typeof(IFieldMapper), mt);
            }
        }

    }

}

