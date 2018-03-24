using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using S2fx.Environment.Extensions.Entity;

namespace S2fx.Model.Metadata.Types {

    public interface IEntityType {
        string Name { get; }
        Task<MetaEntity> LoadAsync(EntityInfo descriptor);
    }

}
