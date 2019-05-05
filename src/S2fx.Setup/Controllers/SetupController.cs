using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace S2fx.Setup.Controllers {

    public class SetupController : Controller {

        public async Task<ActionResult> Index() {
            await Task.CompletedTask;
            return View();
        }

    }

}
