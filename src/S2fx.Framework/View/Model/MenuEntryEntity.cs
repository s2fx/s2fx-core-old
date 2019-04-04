using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using S2fx.Model;
using S2fx.Model.Annotations;
using S2fx.Security.Model;

namespace S2fx.View.Model.Model {

    [Entity(EntityName), DisplayName("Menu Item")]
    public class MenuEntryEntity : AbstractAuditedEntity, IHierarchyEntity<MenuEntryEntity> {
        public const string EntityName = "Core.MenuEntry";

        [Required, MaxLength(256)]
        public virtual string Name { get; set; }

        [MaxLength(256)]
        public virtual string Text { get; set; }

        [Required, MaxLength(256)]
        public virtual string Feature { get; set; }

        [ManyToOneProperty]
        public virtual ActionEntity Action { get; set; }

        public virtual int Order { get; set; }

        [Required]
        public virtual string Definition { get; set; }

        [Required, MaxLength(256)]
        public virtual string DefinitionKey { get; set; }

        [DisplayName("Related Roles")]
        [ManyToManyProperty(mappedBy: "Menus", joinTable: "core_role_menu")]
        public virtual ICollection<RoleEntity> Roles { get; set; }

        [ManyToOneProperty]
        public virtual MenuEntryEntity Parent { get; set; }

        public virtual long RangeLeft { get; set; }

        public virtual long RangeRight { get; set; }
    }

}
