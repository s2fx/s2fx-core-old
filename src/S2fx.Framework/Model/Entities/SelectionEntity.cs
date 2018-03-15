using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Entities {

    [Table("core_selection"), Entity, DisplayName("Selection")]
    public class SelectionEntity : AbstractAuditedEntity {

        public string ModuleName { get; set; }

        public string Name { get; set; }

        public virtual ICollection<SelectionItemEntity> Items { get; set; }
    }

    [Table("core_selection_item"), Entity, DisplayName("Selection Item")]
    public class SelectionItemEntity : AbstractAuditedEntity {

        public virtual SelectionEntity Selection { get; set; }

        public int Order { get; set; }

        public string Value { get; set; }

        public string Label { get; set; }
    }
}
