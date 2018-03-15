using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    public interface IMetaModule {
        string Name { get; }
        string State { get; }
    }

}
