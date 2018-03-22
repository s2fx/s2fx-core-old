using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OrchardCore.Modules;
using System.Reflection;
using S2fx.Data;
using S2fx.Model.Annotations;
using OrchardCore.Environment.Shell;
using S2fx.Environment.Configuration;

namespace S2fx.Web.Controllers {
    public class HomeController : Controller {
        private readonly IDatabaseMigrator _migrator;

        public HomeController(IHostingEnvironment env, IDatabaseMigrator migrator, IConfiguration cfg) {
            _migrator = migrator;
            var x = cfg.GetSection(WellKnownConstants.SlipstreamConfigurationSection).Get<S2Settings>();
        }

        public IActionResult Index() {

            Task.Run(() => _migrator.MigrateSchemeAsync()).Wait();

            return View();
        }
    }
}