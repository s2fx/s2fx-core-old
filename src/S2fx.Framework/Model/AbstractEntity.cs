using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Annotations;
using S2fx.Model.Builtin;
using S2fx.Security.Model;

namespace S2fx.Model {

    public abstract class AbstractEntity : IAuditedEntity, IMutableEntity<AbstractEntity> {

        [IdField]
        public virtual long Id { get; set; }

        [NotField]
        public virtual bool IsPersistent => Id > 0;

        public virtual long _Version { get; set; }

        [ManyToOneField(UserEntity.EntityName)]
        public virtual UserEntity _CreatedBy { get; set; }

        [ManyToOneField(UserEntity.EntityName)]
        public virtual UserEntity _UpdatedBy { get; set; }

        public virtual DateTime _CreatedOn { get; set; }

        public virtual DateTime? _UpdatedOn { get; set; }


        [JsonObjectField]
        public virtual IReadOnlyDictionary<string, object> _ExtraFields { get; set; }
    }

}
