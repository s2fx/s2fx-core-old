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
using S2fx.Model.Metadata.Conventions;
using S2fx.Xaml;
using S2fx.Environment;

namespace Microsoft.Extensions.DependencyInjection {

    public static class ServiceCollectionExtensions {
        public static void AddSlipStreamFramework(this IServiceCollection services) {

            //environment
            services.AddS2Environment();

            services.AddS2fxData();

            //model
            services.AddS2Model();

            //Remoting 
            services.AddRemoting();

            // Xaml 
            services.AddXamlSupport();

            //Setup
            {
                services.AddTransient<ISetupService, SetupService>();
            }

        }




    }
}
