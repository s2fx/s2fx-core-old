using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Metadata {

    /// <summary>
    /// Contains a snapshot of a tenant's entities(in enabled features).
    /// </summary>
    public class ModelDescriptor {

        public IList<MetaEntity> Entities { get; set; } = new List<MetaEntity>();

    }

}
