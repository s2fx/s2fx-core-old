using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using S2fx.Model.Annotations;

namespace S2fx.Model.Builtin {

    [Entity(EntityName), DisplayName("Scheduled Task")]
    public class ScheduledTaskEntity : AbstractAuditedEntity {
        public const string EntityName = "Core.ScheduledTask";

        [Required, MaxLength(256)]
        public virtual string Name { get; set; }

        [MaxLength(256)]
        public virtual string DisplayName { get; set; }

        [Required, MaxLength(256)]
        public virtual string Cron { get; set; }

        [Required]
        public virtual bool Actived { get; set; }

        [Required, MaxLength(256)]
        public virtual string Job { get; set; }

        public virtual string Notes { get; set; }

        [OneToManyField(nameof(ScheduledTaskInstanceEntity.Task))]
        public virtual ICollection<ScheduledTaskInstanceEntity> Instances { get; set; }
    }

}
