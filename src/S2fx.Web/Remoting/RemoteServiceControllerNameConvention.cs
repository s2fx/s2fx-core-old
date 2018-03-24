using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using S2fx.Remoting;

namespace S2fx.Web.Remoting {

    public class RemoteServiceControllerNameConvention : IControllerModelConvention {

        public void Apply(ControllerModel controller) {
            var remoteServiceAttr = controller.ControllerType.GetCustomAttribute<RemoteServiceAttribute>();
            if (remoteServiceAttr != null) {
                controller.ControllerName = remoteServiceAttr.Name;
            }
        }
    }

}
