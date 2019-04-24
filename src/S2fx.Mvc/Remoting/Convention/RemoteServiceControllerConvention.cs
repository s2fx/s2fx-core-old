using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Internal;
using S2fx.Remoting;
using S2fx.Remoting.Model;

namespace S2fx.Mvc.Remoting {

    public class RemoteServiceControllerConvention : AbstractRemoteServiceModelConvention, IControllerModelConvention {

        public RemoteServiceControllerConvention(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) {
        }

        public void Apply(ControllerModel controller) {
            bool isRemoteServiceController = this.RemoteServiceManager.TryGetRemoteService(controller.ControllerType, out var serviceInfo);

            if (isRemoteServiceController) {
                this.ConfigureRemoteServiceControllerName(controller, serviceInfo);

                this.ConfigureRemoteServiceControllerArea(controller, serviceInfo);

                this.ConfigureRemoteServiceControllerActions(controller, serviceInfo);
            }
        }

        private void SetHttpVerbOfAction(RemoteServiceInfo serviceInfo, ActionModel action, RemoteServiceMethodInfo serviceMethod) {
            var selector = action.Selectors.FirstOrDefault();
            if (selector?.ActionConstraints != null && selector?.ActionConstraints.Count == 0) {

                switch (serviceMethod.HttpMethod) {
                    case HttpMethod.Get:
                        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new string[] { "GET" }));
                        break;

                    case HttpMethod.Post:
                        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new string[] { "POST" }));
                        break;

                    case HttpMethod.Delete:
                        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new string[] { "DELETE" }));
                        break;

                    case HttpMethod.Put:
                        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new string[] { "PUT" }));
                        break;

                    case HttpMethod.Patch:
                        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new string[] { "PATCH" }));
                        break;

                    default:
                        throw new NotSupportedException(
                            $"Unsupported HTTP method '{serviceMethod.HttpMethod}' of method '{serviceMethod.Name}' in Remote Service '{serviceInfo.Name}'");
                }
            }
        }

        private void ClearNonRemoteServiceMethods(ControllerModel controller, RemoteServiceInfo remoteService) {

            var serviceMethods = new HashSet<MethodInfo>(remoteService.Methods.Select(x => x.ClrMethodInfo));

            //从控制器的动作里删掉所有的方法
            var actionIndicesToDelete = new List<int>();
            for (var i = 0; i < controller.Actions.Count; i++) {
                var oldAction = controller.Actions[i];
                if (!serviceMethods.Contains(oldAction.ActionMethod)) {
                    actionIndicesToDelete.Add(i);
                }
            }

            foreach (var i in actionIndicesToDelete) {
                controller.Actions.RemoveAt(i);
            }
        }

        private void ConfigureRemoteServiceControllerArea(ControllerModel controller, RemoteServiceInfo serviceInfo) {
            controller.RouteValues["area"] = serviceInfo.Area;
        }

        private void ConfigureRemoteServiceControllerName(ControllerModel controller, RemoteServiceInfo serviceInfo) {
            controller.ControllerName = serviceInfo.Name;
        }

        private void ConfigureRemoteServiceControllerActions(ControllerModel controller, RemoteServiceInfo serviceInfo) {
            this.ClearNonRemoteServiceMethods(controller, serviceInfo);
            var serviceMethods = serviceInfo.Methods.ToDictionary(x => x.ClrMethodInfo);
            foreach (var action in controller.Actions) {
                var serviceMethod = serviceMethods[action.ActionMethod];
                this.SetHttpVerbOfAction(serviceInfo, action, serviceMethod);

                if (action.ActionName.EndsWith("Async")) {
                    action.ActionName = action.ActionName.Substring(0, action.ActionName.Length - 5);
                }
            }
        }

    }

}
