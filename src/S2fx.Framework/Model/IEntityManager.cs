using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.Environment.Extensions.Entity;
using S2fx.Model.Metadata;

namespace S2fx.Model {

    /// <summary>
    /// The interface to manage Entities
    /// </summary>
    public interface IEntityManager {

        MetaEntity GetEntityByClrType(Type clrType);

        MetaEntity GetEntity(string fullName);

        IReadOnlyDictionary<string, MetaEntity> GetEntities();

        Task<IEnumerable<EntityInfo>> LoadEntitiesAsync();
    }

}
