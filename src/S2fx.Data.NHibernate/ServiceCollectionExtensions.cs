using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NHibernate;
using NHibernate.Cfg;
using OrchardCore.Modules;
using S2fx.Data;
using S2fx.Data.NHibernate;
using S2fx.Data.NHibernate.Mapping;
using S2fx.Data.NHibernate.Mapping.Properties;
using S2fx.Data.NHibernate.UnitOfWork;
using S2fx.Data.UnitOfWork;
using S2fx.Utility;

namespace Microsoft.Extensions.DependencyInjection {

    public static class ServiceCollectionExtensions {

        public static void WithNHibernate(this IServiceCollection services) {

            //Unit of work
            //NH stuffs

            //Unit of work
            //services.AddTransient<ISessionFactory, NH.Cfg.Configuration>();
            services.AddScoped<IUnitOfWork, HibernateUnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(DefaultHibernateRepository<>));

            //entity mapping
            services.AddTransient(typeof(EntityMappingClass<>), typeof(EntityMappingClass<>));
            services.AddTransient<IModelMapper, ModelMapper>();
            AddBuiltinFieldMappers(services);

            //Register nhibernate ISessionFactory 
            services.AddTransient<IHibernateConfigurationFactory, HibernateConfigurationFactory>();

            //Register NH's Configuration to container
            services.AddSingletonLazy(sp => sp.GetRequiredService<IHibernateConfigurationFactory>().Create());

            //Register NH's ISessionFactory 
            services.AddSingletonLazy(sp => sp.GetRequiredService<NHibernate.Cfg.Configuration>().BuildSessionFactory());

            services.AddScoped(sp => sp.GetRequiredService<ISessionFactory>().OpenSession());

            //migrator
            services.AddScoped<IDbMigrator, HibernateDbMigrator>();
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

