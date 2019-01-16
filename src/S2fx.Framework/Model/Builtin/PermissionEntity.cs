using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Builtin {

    [Entity("Core.Permission"), DisplayName("Permission")]
    public class PermissionEntity : AbstractAuditedEntity {

        [Required, MaxLength(256)]
        public virtual string Name { get; set; }

        [MaxLength(256)]
        public virtual string DisplayName { get; set; }

        public virtual bool CanCreate { get; set; }

        public virtual bool CanReadSingle { get; set; }

        public virtual bool CanReadMany { get; set; } = false;

        public virtual bool CanUpdate { get; set; }

        public virtual bool CanDelete { get; set; }

        [ManyToManyProperty(mappedBy: "Permissions", joinTable: "core_role_permission")]
        public virtual ICollection<RoleEntity> Roles { get; set; }

    }

}
