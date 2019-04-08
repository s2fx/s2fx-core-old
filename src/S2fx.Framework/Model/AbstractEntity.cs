using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Annotations;
using S2fx.Model.Builtin;
using S2fx.Security.Model;

namespace S2fx.Model {

    public abstract class AbstractEntity : IEntity {

        [IdField]
        public virtual long Id { get; set; }

        public virtual bool IsPersistent => Id > 0;
    }

    public abstract class AbstractAuditedEntity : AbstractEntity, IAuditedEntity {

        [ManyToOneField(UserEntity.EntityName)]
        public virtual UserEntity CreatedBy { get; set; }

        [ManyToOneField(UserEntity.EntityName)]
        public virtual UserEntity UpdatedBy { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual DateTime? UpdatedOn { get; set; }

        [JsonObjectField]
        public virtual IReadOnlyDictionary<string, object> ExtraFields { get; set; }
    }
}
