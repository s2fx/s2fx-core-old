using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public interface IClrTypeEntityMetadataLoader {
        EntityInfo LoadClrType(Type entityType);
    }
}
