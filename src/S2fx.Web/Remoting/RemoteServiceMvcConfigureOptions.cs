using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace S2fx.Web.Remoting {

    public class RemoteServiceMvcConfigureOptions : IConfigureOptions<MvcOptions> {
        private readonly IServiceProvider _services;

        public RemoteServiceMvcConfigureOptions(IServiceProvider services) {
            _services = services;
        }

        public void Configure(MvcOptions options) {
            this.AddControllerConventions(options);
            this.AddModelBinderProviders(options);
        }

        private void AddControllerConventions(MvcOptions options) {
            var conventions = _services.GetServices<IControllerModelConvention>();

            foreach (var convention in conventions) {
                options.Conventions.Add(convention);
            }
        }

        private void AddModelBinderProviders(MvcOptions options) {
            var providers = _services.GetServices<IModelBinderProvider>();

            foreach (var provider in providers) {
                options.ModelBinderProviders.Insert(0, provider);
            }
        }
    }

}
