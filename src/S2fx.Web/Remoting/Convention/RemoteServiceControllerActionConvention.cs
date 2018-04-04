using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using S2fx.Remoting;
using S2fx.Remoting.Model;

namespace S2fx.Web.Remoting {

    public class RemoteServiceControllerActionConvention : AbstractRemoteServiceControllerModelConvention {

        public RemoteServiceControllerActionConvention(IServiceProvider services) : base(services) {

        }

        protected override void ApplyOnRemoteServiceController(ControllerModel controller, RemoteServiceInfo remoteService) {
            controller.Actions.Clear();
            foreach (var serviceMethod in remoteService.Methods) {
                var action = new ActionModel(serviceMethod.ClrMethodInfo, serviceMethod.ClrMethodInfo.GetCustomAttributes().ToList());
                controller.Actions.Add(action);
            }
        }


    }

}
