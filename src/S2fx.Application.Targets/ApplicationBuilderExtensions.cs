using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using S2fx.Mvc;

namespace S2fx {

    public static class ApplicationBuilderExtensions {
        public static IApplicationBuilder UseS2fx(this IApplicationBuilder app) {

            app.UseOrchardCore();

            return app;
        }
    }

}
