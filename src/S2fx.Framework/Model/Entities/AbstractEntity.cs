using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Entities {

    public abstract class AbstractEntity : IEntity {

        public long Id { get; set; }

        public bool IsPersistent => Id <= 0;
    }

    public abstract class AbstractAuditedEntity : AbstractEntity, IAuditedEntity {

        public UserEntity CreatedBy { get; set; }

        public UserEntity UpdatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }

    }
}
