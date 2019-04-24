using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace S2fx.Mvc.Remoting {

    public class RemoteServiceControllerActionParameterConvention : AbstractRemoteServiceModelConvention, IParameterModelConvention {

        public RemoteServiceControllerActionParameterConvention(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) {
        }

        public void Apply(ParameterModel parameter) {

        }

    }

}
