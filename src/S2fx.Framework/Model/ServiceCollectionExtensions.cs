using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Descriptor;
using OrchardCore.Modules;
using S2fx.Conventions;
using S2fx.Data.Seeding;
using S2fx.Data.UnitOfWork;
using S2fx.Environment.Extensions;
using S2fx.Environment.Extensions.Entity;
using S2fx.Environment.Shell;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Conventions;
using S2fx.Model.Services;
using S2fx.Remoting;

namespace S2fx.Model {

    public static class ServiceCollectionExtensions {

        public static void AddS2Model(this IServiceCollection services) {

            services.AddSingleton<IEntityManager, EntityManager>();

            services.RegisterAllBuiltinEntityMetadataConventions();

            //meta data
            services.RegisterAllBuiltinEntityTypes();
            services.RegisterAllBuiltinEntityFieldTypes();

            //visitors
            services.AddTransient<ConventionMetadataVisitor>();

            //internal services
            services.AddTransient<ISequenceService, SequenceService>();
        }

    }

}
