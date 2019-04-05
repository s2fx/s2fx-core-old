using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace S2fx.Web.Remoting {

    public class RemoteServiceControllerActionParameterConvention : AbstractRemoteServiceModelConvention, IParameterModelConvention {

        public RemoteServiceControllerActionParameterConvention(IServiceProvider services) : base(services) {
        }

        public void Apply(ParameterModel parameter) {

        }

    }

}
