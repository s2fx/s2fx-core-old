using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Annotations;
using S2fx.Model.Builtin;

namespace S2fx.Model {

    public abstract class AbstractEntity : IEntity {

        [IdProperty]
        public virtual long Id { get; set; }

        public virtual bool IsPersistent => Id > 0;
    }

    public abstract class AbstractAuditedEntity : AbstractEntity, IAuditedEntity {

        [ManyToOneProperty(UserEntity.EntityName)]
        public virtual UserEntity CreatedBy { get; set; }

        [ManyToOneProperty(UserEntity.EntityName)]
        public virtual UserEntity UpdatedBy { get; set; }

        public virtual DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual DateTime? UpdatedOn { get; set; }

    }
}
