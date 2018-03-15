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

namespace S2fx.Web.Controllers {
    public class HomeController : Controller {
        public HomeController(IHostingEnvironment env, IRepository<ModuleEntity> repo) {
            var app = ModularApplicationContext.GetApplication(env);
            Console.WriteLine("----------------------------------------");
            foreach (var m in app.ModuleNames) {
                Console.WriteLine(m);
            }
            var t = repo.All();
            Console.WriteLine("----------------------------------------");
            foreach (var ea in app.Assembly.GetCustomAttributes<EntityAttribute>()) {
                Console.WriteLine(ea.Name);
            }
        }

        public IActionResult Index() {
            return View();
        }
    }
}