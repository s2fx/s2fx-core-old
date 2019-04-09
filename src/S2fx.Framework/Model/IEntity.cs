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

        UserEntity _CreatedBy { get; set; }
        UserEntity _UpdatedBy { get; set; }
        DateTime _CreatedOn { get; set; }
        DateTime? _UpdatedOn { get; set; }
    }

    public interface IArchivableEntity<TEntity>
        where TEntity : class, IEntity {

        bool _Archived { get; set; }
    }

    public interface ISoftDeletableEntity<TEntity>
        where TEntity : class, IEntity {

        bool _IsDeleted { get; set; }
    }

    public interface IHierarchyEntity<TEntity>
        where TEntity : class, IEntity {

        TEntity _Parent { get; set; }
        long _RangeLeft { get; set; }
        long _RangeRight { get; set; }
    }

    public interface IMutableEntity<TEntity>
        where TEntity : class, IEntity {

        long _Version { get; set; }
    }

}
