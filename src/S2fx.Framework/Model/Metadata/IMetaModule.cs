using S2fx.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {
    public interface IMetaModule : IEntity {
        string Name { get; }
        string State { get; }
    }
}
