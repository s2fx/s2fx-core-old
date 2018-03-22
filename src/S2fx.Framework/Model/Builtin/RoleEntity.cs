using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using S2fx.Model.Annotations;

namespace S2fx.Model.Builtin {

    [Entity(EntityName), DisplayName("Role")]
    public class RoleEntity : AbstractAuditedEntity {
        public const string EntityName = "Core.Role";

        [Required]
        public virtual string Name { get; set; }

        public virtual string DisplayName { get; set; }

        [ManyToManyProperty(mappedBy: "Roles", joinTable: "core_user_role")]
        public virtual ICollection<UserEntity> Users { get; set; }
    }

}
