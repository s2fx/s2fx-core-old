using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using OrchardCore.Logging;

namespace S2fx.Demo.Web {
    public class Program {
        public static Task Main(string[] args) {
            return BuildHost(args).RunAsync();
        }

        public static IHost BuildHost(string[] args) {
            var host = Host.CreateDefaultBuilder(args)
                            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                            //.UseNLogWeb()
                            .Build();

            return host;
        }

    }
}
