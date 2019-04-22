using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace S2fx.Mvc {

    public static class OrchardCoreBuilderExtensions {
        /// <summary>
        /// Adds tenant level MVC services and configuration.
        /// </summary>
        public static OrchardCoreBuilder AddS2fxMvc(this OrchardCoreBuilder builder) {
            return builder.RegisterStartup<Startup>();
        }
    }

}
