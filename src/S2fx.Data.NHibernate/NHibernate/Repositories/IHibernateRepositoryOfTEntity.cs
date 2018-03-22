using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using S2fx.Model;

namespace S2fx.Data.NHibernate {

    public interface IHibernateRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity {

        ISession DbSession { get; }
    }



}
