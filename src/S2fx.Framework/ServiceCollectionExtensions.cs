using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Data;
using S2fx.Data.Convention;
using S2fx.Data.UnitOfWork;
using S2fx.Environment.Extensions;
using S2fx.Environment.Extensions.Entity;
using S2fx.Model;
using S2fx.Model.Metadata.Loaders;

namespace Microsoft.Extensions.DependencyInjection {

    public static class ServiceCollectionExtensions {
        public static void AddS2fx(this IServiceCollection services) {
            //environment
            {
                services.AddTransient<IEntityHarvester, EntityHarvester>();
                services.AddTransient<IEntityMetadataProvider, ModuleEntityMetadataProvider>();
                services.AddTransient<IEntityMetadataProvider, BuiltInEntityMetadataProvider>();
            }

            //Data accessing
            {
                services.AddTransient<IDynamicEntityRepositoryResolver, DynamicEntityRepositoryResolver>();
                services.AddTransient<IUnitOfWorkManager, DefaultUnitOfWorkManager>();
                services.AddTransient<IDbNameConvention, S2DbNameConvention>();
            }

            //model
            {
                services.AddSingleton<IEntityManager, EntityManager>();
                services.AddTransient<IClrTypeEntityMetadataLoader, ClrTypeEntityMetadataLoader>();
            }
        }


    }
}
