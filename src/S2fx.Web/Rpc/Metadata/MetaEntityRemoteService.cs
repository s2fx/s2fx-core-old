using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using S2fx.Model;
using S2fx.Model.Metadata;
using S2fx.Remoting;

namespace S2fx.Web.Rpc.Metadata {

    [RemoteService(Name = "MetaEntity")]
    public class MetaEntityRemoteService {
        private readonly IEntityManager _entityManager;

        public MetaEntityRemoteService(IEntityManager entityManager) {
            _entityManager = entityManager;
        }

        public IReadOnlyDictionary<string, MetaEntity> All() {
            return _entityManager.GetEntities();
        }

        public MetaEntity Single(string name) {
            return _entityManager.GetEntity(name);
        }
    }

}
