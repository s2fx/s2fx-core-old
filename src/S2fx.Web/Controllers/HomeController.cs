using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Modules;
using S2fx.Data;
using S2fx.Metadata.Model;

namespace S2fx.Web.Controllers {
    public class HomeController : Controller {
        public HomeController(IHostingEnvironment env, IRepository<MetaEntity> repo) {
            var app = ModularApplicationContext.GetApplication(env);
            Console.WriteLine("----------------------------------------");
            foreach (var m in app.ModuleNames) {
                Console.WriteLine(m);
            }
            var t = repo.All();
        }

        public IActionResult Index() {
            return View();
        }
    }
}