using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using OrchardCore.Logging;

namespace S2fx.Demo.Web {
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
