using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Builtin {

    [Entity(EntityName), DisplayName("Feature")]
    public class FeatureEntity : AbstractAuditedEntity {

        public const string EntityName = "Core.Feature";

        [Required]
        public virtual string Name { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual bool IsInstalled { get; set; }

        public virtual bool IsEnabled { get; set; }

        [ManyToOneProperty]
        public virtual ModuleEntity Module { get; set; }

    }


}
