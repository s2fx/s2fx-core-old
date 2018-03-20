using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Entities {

    public class UserRole {

        [ManyToOneProperty(RoleEntity.EntityName)]
        public RoleEntity Role { get; set; }

        [ManyToOneProperty(RoleEntity.EntityName)]
        public UserEntity User { get; set; }

    }

}
