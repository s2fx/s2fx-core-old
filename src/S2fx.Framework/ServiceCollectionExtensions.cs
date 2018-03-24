using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using S2fx.Data;
using S2fx.Convention;
using S2fx.Data.UnitOfWork;
using S2fx.Environment.Extensions;
using S2fx.Environment.Extensions.Entity;
using S2fx.Model;
using S2fx.Utility;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;
using S2fx.Setup.Services;
using S2fx.Remoting;
using S2fx.Environment.Extensions.Remoting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OrchardCore.Environment.Shell.Descriptor;
using OrchardCore.Environment.Shell;
using S2fx.Environment.Shell;
using S2fx.Model.Metadata.Conventions;

namespace Microsoft.Extensions.DependencyInjection {

    public static class ServiceCollectionExtensions {
        public static void AddSlipStreamFramework(this IServiceCollection services) {
            //environment
            {
                services.AddTransient<IEntityHarvester, BuiltinEntityHarvester>();
                services.AddTransient<IEntityHarvester, ClrEntityHarvester>();

                services.AddSingleton<IS2ModuleManager, S2ModuleManager>();
                services.Replace(new ServiceDescriptor(typeof(IShellDescriptorManager), typeof(S2ShellDescriptorManager), ServiceLifetime.Scoped));
                services.Replace(new ServiceDescriptor(typeof(IShellStateManager), typeof(S2ShellStateManager), ServiceLifetime.Scoped));
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

                services.RegisterBuiltinMetadataConventions();

                //meta data
                services.RegisterAllEntityTypes();
                services.RegisterAllPropertyTypes();
            }

            //Remoting 
            {
                services.AddSingleton<IRemoteServiceManager, RemoteServiceManager>();
                services.AddTransient<IRemoteServiceMetadataProvider, ModuleAssemblyRemoteServiceMetadataProvider>();
            }

            //Setup
            {
                services.AddTransient<ISetupService, SetupService>();
            }

        }




    }
}
