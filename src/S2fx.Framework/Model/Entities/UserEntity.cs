using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Entities {

    [Entity(EntityName), DisplayName("User")]
    public class UserEntity : AbstractAuditedEntity {
        public const string EntityName = "Core.User";

        [Required]
        public string Login { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Password { get; set; }


        public byte[] Image { get; set; }

        public string Email { get; set; }
    }

}
