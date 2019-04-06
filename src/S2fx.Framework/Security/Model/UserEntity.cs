using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using OrchardCore.Users;
using S2fx.Model;
using S2fx.Model.Annotations;

namespace S2fx.Security.Model {

    [Entity(EntityName), DisplayName("User")]
    public class UserEntity : AbstractAuditedEntity, IUser {
        public const string EntityName = "Core.User";

        [Required, MaxLength(64), DisplayName("User Name")]
        public virtual string UserName { get; set; }

        [Required, MaxLength(64), DisplayName("Full Name")]
        public virtual string FullName { get; set; }

        [Password, Hashed, Required, MaxLength(256), DisplayName("Password")]
        public virtual string Password { get; set; }

        [DisplayName("Image"), Lazy]
        public virtual byte[] Image { get; set; }

        [MaxLength(256), DisplayName("E-Mail")]
        public virtual string Email { get; set; }

        [DisplayName("Related Users")]
        [ManyToManyField(mappedBy: "Users", joinTable: "core_user_role")]
        public virtual ICollection<RoleEntity> Roles { get; set; }
    }

}
