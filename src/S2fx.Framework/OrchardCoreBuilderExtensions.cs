using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using S2fx.Data;
using S2fx.Data.UnitOfWork;
using S2fx.Environment.Extensions;
using S2fx.Model;
using S2fx.Utility;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;
using S2fx.Setup.Services;
using S2fx.Remoting;
using S2fx.Model.Metadata.Conventions;
using S2fx.Xaml;
using S2fx.Environment;
using OrchardCore.DeferredTasks;
using S2fx.Services;
using S2fx.Security;

namespace S2fx {

    public static class OrchardCoreBuilderExtensions {

        public static OrchardCoreBuilder AddS2Framework(this OrchardCoreBuilder builder) {

            //global services
            {
                var services = builder.ApplicationServices;
                services.AddS2EnvironmentGlobal();

                services.AddS2ServicesGlobal();

                services.AddS2fxDataGlobal();

                // Xaml 
                services.AddXamlSupportGlobal();
            }

            //tenant services
            builder.AddDeferredTasks();

            return builder.ConfigureServices(services => {
                //environment
                services.AddS2EnvironmentTenant();

                services.AddS2fxDataTenant();

                services.AddS2Security();

                //model
                services.AddS2Model();

                //Remoting 
                services.AddRemotingTenants();
                services.AddInternalRemoteServicesTenants();

                //Setup
                {
                    services.AddTransient<ISetupService, SetupService>();
                }
            });
        }

    }
}
