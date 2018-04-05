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
            var actionIndicesToDelete = new List<int>();
            for (var i = 0; i < controller.Actions.Count; i++) {
                var oldAction = controller.Actions[i];
                if (!remoteService.Methods.Any(x => x.ClrMethodInfo == oldAction.ActionMethod)) {
                    actionIndicesToDelete.Add(i);
                }
            }

            foreach (var i in actionIndicesToDelete) {
                controller.Actions.RemoveAt(i);
            }
        }


    }

}
