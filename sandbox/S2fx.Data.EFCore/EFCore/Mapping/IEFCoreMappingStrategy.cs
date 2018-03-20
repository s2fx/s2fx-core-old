using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using S2fx.Model.Metadata;

namespace S2fx.Data.EFCore.Mapping {

    public interface IEFCoreMappingStrategy {
        void MapEntity(ModelBuilder modelBuilder, EntityInfo entityInfo);
    }

}
