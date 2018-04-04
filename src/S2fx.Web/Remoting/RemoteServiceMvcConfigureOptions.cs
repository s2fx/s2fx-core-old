using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace S2fx.Web.Remoting {

    public class RemoteServiceMvcConfigureOptions : IConfigureOptions<MvcOptions> {
        private readonly IServiceProvider _services;

        public RemoteServiceMvcConfigureOptions(IServiceProvider services) {
            _services = services;
        }

        public void Configure(MvcOptions options) {
            var conventions = _services.GetServices<IControllerModelConvention>();

            foreach (var convention in conventions) {
                options.Conventions.Add(convention);
            }
        }

    }

}
