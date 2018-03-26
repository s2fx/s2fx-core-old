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
        private readonly IServiceProvider _services;

        public RemoteServiceControllerFeatureProvider(IServiceProvider services) {
            _services = services;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature) {
            var harvester = _services.GetRequiredService<IRemoteServiceManager>();
            var remoteServices = Task.Run(harvester.LoadRemoteServicesAsync).Result;

            foreach (var rs in remoteServices) {
                feature.Controllers.Add(rs.ClrType);
            }
        }

    }

}
