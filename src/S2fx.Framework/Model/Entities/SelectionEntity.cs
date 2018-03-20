using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Entities {

    [Entity(EntityName), DisplayName("Selection")]
    public class SelectionEntity : AbstractAuditedEntity {
        public const string EntityName = "Core.Selection";

        [Required]
        public virtual string ModuleName { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [OneToManyProperty(SelectionItemEntity.EntityName, nameof(SelectionItemEntity.Selection))]
        public virtual ICollection<SelectionItemEntity> Items { get; set; }
    }

    [Entity(EntityName), DisplayName("Selection Item")]
    public class SelectionItemEntity : AbstractAuditedEntity {
        public const string EntityName = "Core.SelectionItem";

        [ManyToOneProperty(SelectionEntity.EntityName, nameof(SelectionEntity.Items)), Required]
        public virtual SelectionEntity Selection { get; set; }

        [Required]
        public virtual int Order { get; set; }

        [Required]
        public virtual string Value { get; set; }

        [Required]
        public virtual string Label { get; set; }
    }
}
