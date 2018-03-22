using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Builtin {

    [Entity("Core.Permission"), DisplayName("Permission")]
    public class PermissionEntry : AbstractAuditedEntity {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool CanCreate { get; set; }
        public bool CanReadSingle { get; set; }
        public bool CanReadMany { get; set; } = false;
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }

}
