using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using OrchardCore.Environment.Shell;
using System.IO;
using Microsoft.Extensions.Options;

namespace S2fx.Web {
    public class Startup : StartupBase {
        public const string NgClientUrlPrefix = "ng-client";

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) {
            _configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services) {

        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider) {
            var shellOptions = serviceProvider.GetRequiredService<IOptions<ShellOptions>>();
            var shellSettings = serviceProvider.GetRequiredService<ShellSettings>();

            var clientApp = this.GetClientAppPath(shellOptions.Value, shellSettings);

            /*
            if (!Directory.Exists(mediaPath)) {
                Directory.CreateDirectory(mediaPath);
            }
            */

            /*
            // ImageSharp before the static file provider
            app.UseImageSharp();
            */

            /*
            var x = Module.WebRootPath;
            app.UseStaticFiles(new StaticFileOptions {
                // The tenant's prefix is already implied by the infrastructure
                RequestPath = "ClientApp",
                FileProvider = new PhysicalFileProvider(clientApp),
                ServeUnknownFileTypes = true,
            });
            */
        }

        private string GetClientAppPath(ShellOptions shellOptions, ShellSettings shellSettings) {
            return Path.Combine(shellOptions.ShellsApplicationDataPath, shellOptions.ShellsContainerName, shellSettings.Name, "wwwroot");
        }
    }
}