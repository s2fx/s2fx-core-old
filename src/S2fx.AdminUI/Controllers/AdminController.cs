using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace S2fx.AdminUI.Controllers {

    //[Authorize]
    public class AdminController : Controller {
        public AdminController() {

        }

        public async Task<IActionResult> Index() {
            await Task.CompletedTask;
            return Redirect("~/S2fx.AdminUI/dist/index.html");
        }
    }

}
