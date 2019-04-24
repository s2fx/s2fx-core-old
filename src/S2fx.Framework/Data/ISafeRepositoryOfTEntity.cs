using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model;

namespace S2fx.Data {

    public interface ISafeRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity {

        IRepository<TEntity> Sudo();

    }

}
