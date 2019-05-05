using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Models;
using S2fx.Setup.Services;
using S2fx.Setup.ViewModels;

namespace S2fx.Setup.Controllers {

    [Route("System/Setup")]
    [Authorize(AuthenticationSchemes = "Api"), IgnoreAntiforgeryToken, AllowAnonymous]
    [ApiController]
    public class SetupApiController : Controller {

        readonly ISetupService _setupService;
        readonly IShellHost _shellHost;
        readonly IShellSettingsManager _shellSettingsManager;


        public SetupApiController(ISetupService setupService, IShellHost shellHost, IShellSettingsManager shellSettingsManager) {
            _setupService = setupService;
            _shellHost = shellHost;
            _shellSettingsManager = shellSettingsManager;
        }

        [HttpPost]
        [Route("Setup")]
        public async Task<IActionResult> SetupAsync(SetupViewModel model) {

            /*
            if (!IsDefaultShell()) {
                return Unauthorized();
            }
            */

            /*
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.ManageTenants)) {
                return Unauthorized();
            }
            */

            if (!ModelState.IsValid) {
                return BadRequest();
            }

            if (!_shellHost.TryGetSettings(model.TenantName, out var shellSettings)) {
                ModelState.AddModelError(nameof(SetupViewModel.TenantName), string.Format("Tenant not found: '{0}'", model.TenantName));
            }

            if (shellSettings.State == TenantState.Running) {
                return StatusCode(201);
            }

            var setupContext = new SetupContext {
                ShellSettings = shellSettings,
                EnabledFeatures = null, // default list,
            };

            var executionId = await _setupService.SetupAsync(setupContext);
            // Check if a component in the Setup failed
            if (setupContext.IsFailed) {
                foreach (var error in setupContext.Errors) {
                    ModelState.AddModelError(error.Key, error.Value);
                }

                return StatusCode(500, ModelState);
            }

            return Ok(executionId);
        }

    }

}
