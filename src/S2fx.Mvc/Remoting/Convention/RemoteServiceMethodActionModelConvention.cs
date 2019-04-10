using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace S2fx.Mvc.Remoting {

    public class RemoteServiceMethodActionModelConvention : AbstractRemoteServiceModelConvention, IActionModelConvention {

        public RemoteServiceMethodActionModelConvention(IServiceProvider services) : base(services) {
        }

        public void Apply(ActionModel action) {
        }

    }

}
