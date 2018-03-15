using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Entities {

    [Table("core_user"), Entity, DisplayName("User")]
    public class UserEntity : AbstractAuditedEntity {
    }

}
