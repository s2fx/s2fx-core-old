using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using S2fx.Model.Entities;

namespace S2fx.Data.NHibernate {

    public interface INhRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity {

        ISession DbSession { get; }
    }



}
