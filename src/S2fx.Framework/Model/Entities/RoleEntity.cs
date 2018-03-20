using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Entities {

    [Entity(EntityName), DisplayName("Role")]
    public class RoleEntity : AbstractAuditedEntity {
        public const string EntityName = "Core.Role";

        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

}
