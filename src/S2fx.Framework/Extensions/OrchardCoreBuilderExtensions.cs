using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using S2fx.Data;
using S2fx.Environment.Extensions;
using S2fx.Model;
using S2fx.Utility;
using S2fx.Model.Metadata;
using S2fx.Model.Metadata.Types;
using S2fx.Remoting;
using S2fx.Model.Metadata.Conventions;
using S2fx.Xaml;
using S2fx.Environment;
using S2fx.Services;
using S2fx.Security;
using S2fx.View;
using S2fx.Modules;
using OrchardCore.Modules;

namespace S2fx {

    public static class OrchardCoreBuilderExtensions {

        public static OrchardCoreBuilder AddS2Framework(this OrchardCoreBuilder builder) {

            //global services
            {
                var services = builder.ApplicationServices;
                services.AddS2EnvironmentGlobal();

                services.AddS2ServicesGlobal();

                services.AddS2DataAccessGlobal();

                services.AddS2ViewGlobal();

                // Xaml 
                services.AddXamlSupportGlobal();

            }

            builder.ConfigureServices(tenantServices => {
                //environment
                tenantServices.AddS2EnvironmentTenant();

                tenantServices.AddS2ModuleTenantServices();

                tenantServices.AddS2DataAccessTenant();

                tenantServices.AddS2Security();

                //model
                tenantServices.AddS2Model();

                //Remoting 
                tenantServices.AddRemotingTenants();
                tenantServices.AddInternalRemoteServicesTenant();

                tenantServices.AddS2ViewTenant();

            });
            return builder;

        }

    }
}
