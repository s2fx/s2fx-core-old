using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using Microsoft.AspNetCore.Builder;

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
        }

    }
}