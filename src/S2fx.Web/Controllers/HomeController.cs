using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Modules;
using System.Reflection;
using S2fx.Data;
using S2fx.Model.Annotations;
using S2fx.Model.Entities;
using OrchardCore.Environment.Shell;
using S2fx.Model;

namespace S2fx.Web.Controllers {
    public class HomeController : Controller {
        private readonly IDatabaseMigrator _migrator;

        public HomeController(IHostingEnvironment env, IDatabaseMigrator migrator) {
            _migrator = migrator;
        }

        public IActionResult Index() {

            Task.Run(() => _migrator.MigrateSchemeAsync()).Wait();

            return View();
        }
    }
}