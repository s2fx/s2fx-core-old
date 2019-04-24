using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace S2fx.Mvc.Remoting {

    public class RemoteServiceMethodActionModelConvention : AbstractRemoteServiceModelConvention, IActionModelConvention {

        public RemoteServiceMethodActionModelConvention(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) {
        }

        public void Apply(ActionModel action) {
        }

    }

}
