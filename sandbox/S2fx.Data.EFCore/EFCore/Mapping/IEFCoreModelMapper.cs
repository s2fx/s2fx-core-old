using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace S2fx.Data.EFCore.Mapping {

    public interface IEFCoreModelMapper {

        void MapAllEntities(ModelBuilder modelBuilder);

    }

}
