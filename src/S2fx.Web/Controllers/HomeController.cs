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
using Microsoft.Extensions.DependencyInjection;
using S2fx.Web.Rpc.Metadata;
using S2fx.Web.Remoting;
using S2fx.Model.Builtin;

namespace S2fx.Web.Controllers {

    public class HomeController : Controller {
        private readonly IDatabaseMigrator _migrator;

        public HomeController(IHostingEnvironment env, IDatabaseMigrator migrator) {
            _migrator = migrator;
        }

        public async Task<IActionResult> Index() {
            await _migrator.MigrateSchemeAsync();
            return View();
        }
    }
}