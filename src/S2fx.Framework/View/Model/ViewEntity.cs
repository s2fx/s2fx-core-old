using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using S2fx.Model;
using S2fx.Model.Annotations;

namespace S2fx.View.Model.Model {

    [Entity(EntityName), DisplayName("View")]
    public class ViewEntity : AbstractAuditedEntity {
        public const string EntityName = "Core.View";

        [Required, MaxLength(256)]
        public virtual string Name { get; set; }

        [MaxLength(256)]
        public virtual string DisplayName { get; set; }

        [Required, MaxLength(256)]
        public virtual string Feature { get; set; }

        public virtual int Priority { get; set; }

        public virtual string ViewType { get; set; }

        [Required]
        public virtual string Definition { get; set; }

        [Required, MaxLength(256)]
        public virtual string DefinitionKey { get; set; }
    }

}
