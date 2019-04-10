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
using S2fx.Mvc.Remoting;
using S2fx.Model.Builtin;
using S2fx.Data.Seeding;

namespace S2fx.Mvc.Controllers {

    public class HomeController : Controller {
        private readonly IDbMigrator _migrator;
        private readonly IServiceProvider _services;

        public HomeController(IServiceProvider services, IDbMigrator migrator) {
            _services = services;
            _migrator = migrator;
        }

        /*
        public async Task<IActionResult> Index() {
            await Task.CompletedTask;
            //await _migrator.MigrateSchemeAsync();
            //var loader = _services.GetService<ISeedDataLoader>();
            //await loader.LoadAllSeedDataAsync();
            return View();
        }
        */


        public async Task<IActionResult> InitDB() {
            await _migrator.MigrateSchemaAsync();

            try {
                var loader = _services.GetService<ISeedLoader>();
                await loader.LoadAllSeedsAsync();
                return Content("Database initialized.");
            }
            catch (Exception ex) {
                return Content($"Failed to initialize database: {ex.Message}");
            }

        }
    }
}