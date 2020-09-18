using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;

namespace S2fx.AdminUI {

    public class AdminUIOptionsSetup : IConfigureOptions<AdminUIOptions> {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IApplicationContext _applicationContext;

        /// <summary>
        /// Initializes a new instance of <see cref="LiquidViewOptions"/>.
        /// </summary>
        /// <param name="hostingEnvironment"><see cref="IWebHostEnvironment"/> for the application.</param>
        /// <param name="applicationContext"><see cref="IApplicationContext"/> for the application.</param>
        public AdminUIOptionsSetup(IWebHostEnvironment hostingEnvironment, IApplicationContext applicationContext) {
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public void Configure(AdminUIOptions options) {
            if (_hostingEnvironment.ContentRootFileProvider != null) {
                if (_hostingEnvironment.IsDevelopment()) {
                }
            }
        }
    }
}
