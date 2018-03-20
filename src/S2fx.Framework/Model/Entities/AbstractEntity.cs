using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model.Entities {

    public abstract class AbstractEntity : IEntity {

        [IdProperty]
        public long Id { get; set; }

        public bool IsPersistent => Id <= 0;
    }

    public abstract class AbstractAuditedEntity : AbstractEntity, IAuditedEntity {

        [ManyToOneProperty(UserEntity.EntityName)]
        public UserEntity CreatedBy { get; set; }

        [ManyToOneProperty(UserEntity.EntityName)]
        public UserEntity UpdatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

    }
}
