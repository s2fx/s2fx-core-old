using S2fx.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using S2fx.Model.Entities;

namespace S2fx.Data.EFCore {

    public interface IEFRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity {

        DbContext DbContext { get; }

        DbSet<TEntity> Table { get; }
    }

}
