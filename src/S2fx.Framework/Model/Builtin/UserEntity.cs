using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Builtin {

    [Entity(EntityName), DisplayName("User")]
    public class UserEntity : AbstractAuditedEntity {
        public const string EntityName = "Core.User";

        [Required, MaxLength(64)]
        public virtual string Login { get; set; }

        [Required, MaxLength(64)]
        public virtual string FullName { get; set; }

        [Required, MaxLength(256)]
        public virtual string Password { get; set; }


        public virtual byte[] Image { get; set; }

        [MaxLength(256)]
        public virtual string Email { get; set; }

        [ManyToManyProperty(mappedBy: "Users", joinTable: "core_user_role")]
        public virtual ICollection<RoleEntity> Roles { get; set; }
    }

}
