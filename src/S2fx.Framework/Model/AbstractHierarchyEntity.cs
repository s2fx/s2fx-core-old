using System;
using System.Collections.Generic;
using System.Text;
using S2fx.Model.Annotations;

namespace S2fx.Model {

    public abstract class AbstractHierarchyEntity<TEntity> : AbstractEntity, IHierarchyEntity<TEntity>
        where TEntity : class, IEntity, IHierarchyEntity<TEntity> {

        [ManyToOneField]
        public virtual TEntity _Parent { get; set; }

        [Hidden]
        public virtual long _RangeLeft { get; set; }

        [Hidden]
        public virtual long _RangeRight { get; set; }
    }

}
