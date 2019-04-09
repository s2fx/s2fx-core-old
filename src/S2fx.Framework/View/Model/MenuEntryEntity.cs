using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using S2fx.Model;
using S2fx.Model.Annotations;
using S2fx.Security.Model;

namespace S2fx.View.Model.Model {

    [Entity(EntityName), DisplayName("Menu Item")]
    public class MenuItemEntity : AbstractHierarchyEntity<MenuItemEntity> {
        public const string EntityName = "Core.MenuItem";

        [Required, MaxLength(256)]
        public virtual string Name { get; set; }

        [MaxLength(256)]
        public virtual string Text { get; set; }

        [Required, MaxLength(256)]
        public virtual string Feature { get; set; }

        [ManyToOneField]
        public virtual ActionEntity Action { get; set; }

        public virtual int Order { get; set; }

        [Required]
        public virtual string Definition { get; set; }

        [Required, MaxLength(256)]
        public virtual string DefinitionKey { get; set; }

        [DisplayName("Related Roles")]
        [ManyToManyField(mappedBy: nameof(RoleEntity.Menus), joinTable: "core_role_menu")]
        public virtual ICollection<RoleEntity> Roles { get; set; }

    }

}
