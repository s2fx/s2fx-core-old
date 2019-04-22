using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Descriptor;
using OrchardCore.Environment.Shell.Descriptor.Settings;
using OrchardCore.Modules;

using AspNetCore.RouteAnalyzer; // Add

namespace S2fx.Demo.Web {

    public class Startup {
        private readonly IServiceProvider _applicationServices;

        public Startup(IServiceProvider applicationServices, IConfiguration configuration) {
            _applicationServices = applicationServices;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            // Add ASP.NET MVC and support for modules
            services.AddMvc();
            services.AddS2fx(this.Configuration);

            services.AddRouteAnalyzer(); // Add
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {


            if (env.IsDevelopment()) {
                // app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
            }

            app.UseMvc(routes => {
                if (env.IsDevelopment()) {
                    routes.MapRouteAnalyzer("/_routes"); // Add
                    routes.MapRoute(
                        name: "default",
                        template: "{controller}/{action=Index}/{id?}");
                }
            });

            app.UseS2fx();

        }
    }
}
