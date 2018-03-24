using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Builtin {

    [Entity("Core.Permission"), DisplayName("Permission")]
    public class PermissionEntry : AbstractAuditedEntity {

        [Required, MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string DisplayName { get; set; }

        public bool CanCreate { get; set; }

        public bool CanReadSingle { get; set; }

        public bool CanReadMany { get; set; } = false;

        public bool CanUpdate { get; set; }

        public bool CanDelete { get; set; }
    }

}
