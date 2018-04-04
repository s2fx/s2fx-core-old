using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model;
using S2fx.Model.Metadata;
using S2fx.Remoting;
using S2fx.Remoting.RemoteServices.Metadata;

namespace S2fx.Remoting.RemoteServices.Metadata {

    [RemoteService(name: "MetaEntity")]
    public class MetaEntityRemoteService {
        private readonly IEntityManager _entityManager;

        public MetaEntityRemoteService(IEntityManager entityManager) {
            _entityManager = entityManager;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get)]
        public virtual IReadOnlyDictionary<string, MetaEntity> All() {
            return _entityManager.GetEntities();
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get)]
        public virtual MetaEntity Single(string name) {
            return _entityManager.GetEntity(name);
        }
    }

}
