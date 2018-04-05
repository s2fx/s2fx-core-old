using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Serialization;

namespace S2fx.Web.Remoting {

    public class RemoteServiceMvcJsonConfigureOptions : IConfigureOptions<MvcJsonOptions> {
        private readonly IServiceProvider _services;

        public RemoteServiceMvcJsonConfigureOptions(IServiceProvider services) {
            _services = services;
        }

        public void Configure(MvcJsonOptions options) {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        }
    }

}
