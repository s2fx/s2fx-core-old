using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;

namespace S2fx.Model {

    public interface IEntityManager {

        EntityInfo GetEntity(string moduleName, string entityName);
        IEnumerable<EntityInfo> GetEnabledEntities();
    }

}
