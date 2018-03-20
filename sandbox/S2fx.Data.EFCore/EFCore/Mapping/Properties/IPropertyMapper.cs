using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using S2fx.Model.Metadata;

namespace S2fx.Data.EFCore.Mapping.Properties {

    public interface IPropertyMapper {
        EntityPropertyType PropertyType { get; }
        void MapProperty(ModelBuilder model, EntityInfo entityInfo, EntityPropertyInfo entityPropertyInfo);
    }

}
