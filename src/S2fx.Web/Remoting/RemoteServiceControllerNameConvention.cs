using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using S2fx.Remoting;
using S2fx.Remoting.Model;

namespace S2fx.Web.Remoting {

    public class RemoteServiceControllerNameConvention : AbstractRemoteServiceControllerModelConvention {

        public RemoteServiceControllerNameConvention(IServiceProvider services) : base(services) {

        }

        protected override void ApplyOnRemoteServiceController(ControllerModel controller, RemoteServiceInfo remoteService) {
            controller.ControllerName = remoteService.Name;
        }


    }

}
