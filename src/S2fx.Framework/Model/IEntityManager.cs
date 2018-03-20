using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Metadata;

namespace S2fx.Model {

    public interface IEntityManager {

        MetaEntity GetEntityByClrType(Type entityType);
        MetaEntity GetEntity(string fullName);
        IEnumerable<MetaEntity> GetEnabledEntities();
    }

}
