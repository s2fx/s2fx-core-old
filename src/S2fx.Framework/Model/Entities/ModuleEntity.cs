using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;
using S2fx.Model.Metadata;

namespace S2fx.Model.Entities {

    [Entity, Table("core_module"), DisplayName("Module")]
    public class ModuleEntity : AbstractAuditedEntity, IMetaModule {
        public string Name { get; set; }

        public string State { get; set; }
    }

}
