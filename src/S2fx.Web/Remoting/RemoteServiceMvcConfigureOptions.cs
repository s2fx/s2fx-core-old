using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace S2fx.Web.Remoting {

    public class RemoteServiceMvcConfigureOptions : IConfigureOptions<MvcOptions> {
        private readonly IServiceProvider _services;

        public RemoteServiceMvcConfigureOptions(IServiceProvider services) {
            _services = services;
        }

        public void Configure(MvcOptions options) {
            var controllerNameConvention = _services.GetRequiredService<RemoteServiceControllerNameConvention>();
            options.Conventions.Add(controllerNameConvention);
        }

    }

}
