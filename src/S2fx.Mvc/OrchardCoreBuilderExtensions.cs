using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Data;
using S2fx.Mvc.Data.Transactions;

namespace S2fx.Mvc {

    public static class OrchardCoreBuilderExtensions {
        /// <summary>
        /// Adds tenant level MVC services and configuration.
        /// </summary>
        public static OrchardCoreBuilder AddS2fxMvc(this OrchardCoreBuilder builder) {
            return builder.AddMvc()
                          .RegisterStartup<Startup>();
        }

    }

}
