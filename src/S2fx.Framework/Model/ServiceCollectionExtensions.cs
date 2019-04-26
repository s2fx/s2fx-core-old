using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using S2fx.Model.Environment;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Conventions;
using S2fx.Model.Services;

namespace S2fx.Model {

    internal static class ServiceCollectionExtensions {

        internal static void AddS2Model(this IServiceCollection services) {

            services.TryAddEnumerable(new[] {
                ServiceDescriptor.Transient<IEntityHarvester, BuiltinClrEntityHarvester>(),
                ServiceDescriptor.Transient<IEntityHarvester, EnabledFeaturesClrEntityHarvester>()
            });

            services.AddSingleton<IEntityManager, EntityManager>();

            services.AddTransient<IMetadataModelProvider, DefaultClrModelProvider>();

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
