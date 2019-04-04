using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using S2fx.Model.Annotations;

namespace S2fx.Model.Builtin {

    [Entity(EntityName), DisplayName("Scheduled Task Instance")]
    public class ScheduledTaskInstanceEntity : AbstractAuditedEntity {
        public const string EntityName = "Core.ScheduledTaskInstance";

        [ManyToOneField(ScheduledTaskEntity.EntityName, nameof(ScheduledTaskEntity.Instances)), Required]
        public virtual ScheduledTaskEntity Task { get; set; }

        public virtual string Arguments { get; set; }

        public virtual DateTime StartedOn { get; set; }

        public virtual DateTime? EndedOn { get; set; }

    }

}
