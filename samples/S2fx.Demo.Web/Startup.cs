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
using Microsoft.Extensions.Hosting;

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

            //services.AddMvc();

            // Add ASP.NET MVC and support for modules
            services.AddS2fx(builder => {
                builder.AddTenantFeatures("S2fx.SampleModule");
            });

            //services.AddRouteAnalyzer(); // Add
            foreach (var i in services) {
                var t = i.ImplementationType ?? i.ServiceType;
                if (t.FullName.Contains("S2fx")) {
                    Console.WriteLine(t.FullName);
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {


            if (env.IsDevelopment()) {
                // app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
            }

            app.UseS2fx();

            /*
            app.UseMvc(routes => {
                if (env.IsDevelopment()) {
                    routes.MapRouteAnalyzer("/_routes"); // Add
                    routes.MapRoute(
                        name: "default",
                        template: "{controller}/{action=Index}/{id?}");
                }
            });
            */


        }
    }
}
