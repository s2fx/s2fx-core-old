using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Data.Convention;
using S2fx.Data.UnitOfWork;
using S2fx.Environment.Extensions;

namespace Microsoft.Extensions.DependencyInjection {

    public static class ServiceCollectionExtensions {
        public static void AddS2fx(this IServiceCollection services) {
            //environment
            {
                services.AddTransient<IExtensionProvider, ExtensionProvider>();
            }

            //Data accessing
            {
                // Unit of Work
                services.AddTransient<IUnitOfWorkManager, DefaultUnitOfWorkManager>();
                services.AddTransient<IDbNameConvention, S2DbNameConvention>();
            }
        }


    }
}
