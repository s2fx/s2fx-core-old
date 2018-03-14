using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Metadata {

    [Entity, Table("meta_module")]
    public class MetaModuleEntity : AbstractEntity, IMetaModule {
        public string Name { get; set; }

        public string State { get; set; }
    }

}
