using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using S2fx.Remoting;
using S2fx.Remoting.Model;

namespace S2fx.Web.Remoting {

    public class RemoteServiceControllerAreaConvention : AbstractRemoteServiceControllerModelConvention {

        public RemoteServiceControllerAreaConvention(IServiceProvider services) : base(services) {

        }

        protected override void ApplyOnRemoteServiceController(ControllerModel controller, RemoteServiceInfo remoteService) {
            controller.RouteValues["area"] = remoteService.Area;
        }


    }

}
