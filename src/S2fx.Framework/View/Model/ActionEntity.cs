using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using OrchardCore.Users;
using S2fx.Model;
using S2fx.Model.Annotations;

namespace S2fx.View.Model.Model {

    [Entity(EntityName), DisplayName("Action")]
    public class ActionEntity : AbstractEntity {
        public const string EntityName = "Core.Action";

        [Required, MaxLength(256)]
        public virtual string Name { get; set; }

        [Required, MaxLength(256)]
        public virtual string Entity { get; set; }

        [Required, MaxLength(256)]
        public virtual string Feature { get; set; }

        public virtual int Priority { get; set; }

        public virtual string ActionType { get; set; }

        [Required]
        public virtual string Definition { get; set; }

        [Required, MaxLength(256)]
        public virtual string DefinitionKey { get; set; }
    }

}
