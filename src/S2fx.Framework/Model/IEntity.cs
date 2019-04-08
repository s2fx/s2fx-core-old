using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Builtin;
using S2fx.Security.Model;

namespace S2fx.Model {

    public interface IEntity {

        long Id { get; set; }
        bool IsPersistent { get; }
    }

    public interface IAuditedEntity : IEntity {

        UserEntity CreatedBy { get; set; }
        UserEntity UpdatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime? UpdatedOn { get; set; }
    }

    public interface IArchivableEntity<TEntity>
        where TEntity : class, IEntity {

        bool Archived { get; set; }
    }

    public interface IHierarchyEntity<TEntity>
        where TEntity : class, IEntity {

        TEntity Parent { get; set; }
        long RangeLeft { get; set; }
        long RangeRight { get; set; }
    }

    public interface IMutableEntity<TEntity>
        where TEntity : class, IEntity {

        long Version { get; set; }
    }

}
