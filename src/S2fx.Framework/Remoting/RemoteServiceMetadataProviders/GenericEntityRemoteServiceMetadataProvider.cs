using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using OrchardCore.Environment.Extensions;
using S2fx.Environment.Shell;
using S2fx.Model;
using S2fx.Remoting.Model;
using S2fx.Remoting.RemoteServices;

namespace S2fx.Remoting {

    public class GenericEntityRemoteServiceMetadataProvider : AbstractClrTypeBasedRemoteServiceMetadataProvider {
        public const string DefaultEntityRemoteServiceArea = MvcControllerAreas.EntityArea;

        private readonly IHostingEnvironment _environment;
        private readonly IEntityManager _entityManager;

        public GenericEntityRemoteServiceMetadataProvider(IHostingEnvironment environment, IEntityManager entityManager) {
            _entityManager = entityManager;
            _environment = environment;
        }

        public override Task<IEnumerable<RemoteServiceInfo>> GetAllServicesAsync() {
            var entities = _entityManager.GetEntities().Values;

            var descriptors = new List<RemoteServiceInfo>();
            var genericRemoteServiceType = typeof(GenericRestEntityRemoteService<>);

            foreach (var entity in entities) {
                var serviceImplType = genericRemoteServiceType.MakeGenericType(entity.ClrType).GetTypeInfo();
                var serviceMetadata = this.CreateServiceMetadata(entity.Name, entity.Feature, serviceImplType, DefaultEntityRemoteServiceArea);
                descriptors.Add(serviceMetadata);
            }

            return Task.FromResult(descriptors as IEnumerable<RemoteServiceInfo>);
        }
    }
}
