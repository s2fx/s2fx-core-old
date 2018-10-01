using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrchardCore.Logging;
using S2fx.Metadata.Services;
using S2fx.Remoting;
using S2fx.Web.Remoting;

namespace S2fx.Server {
    public class Program {
        public static void Main(string[] args) {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) {
            var host = WebHost.CreateDefaultBuilder(args)
                            .UseStartup<Startup>()
                            .UseNLogWeb()
                            .Build();

            return host;
        }

    }
}
