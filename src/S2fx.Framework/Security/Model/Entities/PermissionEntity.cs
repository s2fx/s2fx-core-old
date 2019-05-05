using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using S2fx.Model;
using S2fx.Model.Annotations;

namespace S2fx.Security.Model {

    [Entity("Core.Permission"), DisplayName("Permission")]
    public class PermissionEntity : AbstractEntity {

        [Required, MaxLength(256), Unique]
        public virtual string Name { get; set; }

        [MaxLength(256)]
        public virtual string Description { get; set; }

        [MaxLength(256)]
        public virtual string Target { get; set; }

        public virtual bool CanCreate { get; set; } = false;

        public virtual bool CanRead { get; set; } = false;

        public virtual bool CanUpdate { get; set; } = false;

        public virtual bool CanDelete { get; set; } = false;

        public virtual string Filter { get; set; }

        [ManyToManyField(mappedBy: "Permissions", joinTable: "core_role_permission")]
        public virtual ICollection<RoleEntity> Roles { get; set; }

    }

}
