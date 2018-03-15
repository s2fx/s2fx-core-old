using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public interface IMetaEntity {
        string Name { get; }

        ICollection<IMetaModule> Properties { get; }
    }

}
