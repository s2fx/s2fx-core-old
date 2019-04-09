using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Builtin {

    [Entity(EntityName), DisplayName("Selection")]
    public class SelectionEntity : AbstractEntity {
        public const string EntityName = "Core.Selection";

        [Required, MaxLength(256)]
        public virtual string Feature { get; set; }

        [Required, MaxLength(256)]
        public virtual string Name { get; set; }

        [Required, MaxLength(256)]
        public virtual string DisplayName { get; set; }

        [OneToManyField(nameof(SelectionItemEntity.Selection))]
        public virtual ICollection<SelectionItemEntity> Items { get; set; }
    }

    [Entity(EntityName), DisplayName("Selection Item")]
    public class SelectionItemEntity : AbstractEntity {
        public const string EntityName = "Core.SelectionItem";

        [ManyToOneField(SelectionEntity.EntityName, nameof(SelectionEntity.Items)), Required]
        public virtual SelectionEntity Selection { get; set; }

        [Required]
        public virtual int SequenceOrder { get; set; }

        [Required, MaxLength(256)]
        public virtual string Value { get; set; }

        [Required, MaxLength(256)]
        public virtual string Label { get; set; }
    }
}
