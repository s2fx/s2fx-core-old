using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model.Entities {

    public interface IEntity {
        long Id { get; set; }
        bool IsPersistent { get; }
    }

    public interface IAuditedEntity : IEntity {

        UserEntity CreatedBy { get; set; }
        UserEntity UpdatedBy { get; set; }
        DateTimeOffset CreatedOn { get; set; }
        DateTimeOffset UpdatedOn { get; set; }
    }

}
