using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model;
using S2fx.Model.Metadata;
using S2fx.Remoting;
using S2fx.Remoting.RemoteServices.Metadata;

namespace S2fx.Remoting.RemoteServices.Metadata {

    [RemoteService(name: "MetaEntity", Area = MvcControllerAreas.MetadataArea)]
    public class MetaEntityRemoteService {
        private readonly IEntityManager _entityManager;

        public MetaEntityRemoteService(IEntityManager entityManager) {
            _entityManager = entityManager;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: true)]
        public virtual IEnumerable<MetaEntity> All() {
            return _entityManager.GetEntities().Values;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: true)]
        public virtual MetaEntity Single([Url]string name) {
            return _entityManager.GetEntity(name);
        }
    }

}
