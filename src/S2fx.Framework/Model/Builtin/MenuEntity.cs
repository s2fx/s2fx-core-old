using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using S2fx.Model.Annotations;

namespace S2fx.Model.Builtin {

    [Entity(EntityName), DisplayName("Menu Item")]
    public class MenuEntity : AbstractAuditedEntity, IHierarchyEntity<MenuEntity> {
        public const string EntityName = "Core.Menu";

        [Required, MaxLength(256)]
        public virtual string Name { get; set; }

        [MaxLength(256)]
        public virtual string DisplayName { get; set; }

        [Required, MaxLength(256)]
        public virtual string Feature { get; set; }

        public virtual long Action { get; set; }

        public virtual int SequenceOrder { get; set; }

        [Required]
        public virtual string Definition { get; set; }

        [ManyToOneProperty]
        public virtual MenuEntity Parent { get; set; }

        public virtual long RangeLeft { get; set; }

        public virtual long RangeRight { get; set; }
    }

}
