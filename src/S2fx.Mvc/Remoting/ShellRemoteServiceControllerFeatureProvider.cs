using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using S2fx.Remoting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace S2fx.Mvc.Remoting {

    public class RemoteServiceControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature> {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RemoteServiceControllerFeatureProvider(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature) {
            var remoteServiecManager = _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IRemoteServiceManager>();
            var remoteServices = Task.Run(remoteServiecManager.LoadRemoteServicesAsync).Result;
            foreach (var rs in remoteServices) {
                feature.Controllers.Add(rs.ClrType);
            }
        }

    }

}
