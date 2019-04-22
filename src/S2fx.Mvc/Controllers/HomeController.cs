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
        private readonly IServiceProvider _services;

        public HomeController(IServiceProvider services) {
            _services = services;
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
            var migrator = _services.GetRequiredService<IDbMigrator>();
            await migrator.MigrateSchemaAsync();

            try {
                var loader = _services.GetService<ISeedLoader>();
                await loader.LoadAllSeedsAsync();
                return Content("Database initialized.");
            }
            catch (Exception ex) {
                return Content($"Failed to initialize database: {ex.Message}");
            }

        }

        public async Task<IActionResult> LoadSeeds() {
            try {
                var loader = _services.GetRequiredService<ISeedLoader>();
                await loader.LoadAllSeedsAsync();
                return Content("Database initialized.");
            }
            catch (Exception ex) {
                return Content($"Failed to initialize database: {ex.Message}");
            }

        }
    }
}