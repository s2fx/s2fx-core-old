using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Remoting;
using S2fx.Remoting.Model;

namespace S2fx.Web.Remoting {

    public abstract class AbstractRemoteServiceControllerModelConvention : IControllerModelConvention {

        protected IServiceProvider Services { get; }

        public AbstractRemoteServiceControllerModelConvention(IServiceProvider services) {
            this.Services = services;
        }

        public void Apply(ControllerModel controller) {
            var manager = this.Services.GetRequiredService<IRemoteServiceManager>();
            var remoteServiceAttr = controller.ControllerType.GetCustomAttribute<RemoteServiceAttribute>();
            if (remoteServiceAttr != null) {
                var remoteService = manager.GetRemoteService(remoteServiceAttr.Name);
                this.ApplyOnRemoteServiceController(controller, remoteService);
            }
        }

        protected abstract void ApplyOnRemoteServiceController(ControllerModel controller, RemoteServiceInfo remoteService);

    }
}
