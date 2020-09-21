using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace S2fx {

    public static class ApplicationBuilderExtensions {
        public static IApplicationBuilder UseS2fx(this IApplicationBuilder app) {

            app.UseOrchardCore();

            return app;
        }
    }

}
