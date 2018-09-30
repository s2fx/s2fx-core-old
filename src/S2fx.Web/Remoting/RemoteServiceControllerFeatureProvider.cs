using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using S2fx.Remoting;
using System.Threading.Tasks;

namespace S2fx.Web.Remoting {

    public class RemoteServiceControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature> {
        private readonly IRemoteServiceManager _remoteServiceManager;

        public RemoteServiceControllerFeatureProvider(IRemoteServiceManager remoteServiceManager) {
            _remoteServiceManager = remoteServiceManager;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature) {
            var remoteServices = Task.Run(_remoteServiceManager.LoadRemoteServicesAsync).Result;

            foreach (var rs in remoteServices) {
                feature.Controllers.Add(rs.ClrType);
            }
        }

    }

}
