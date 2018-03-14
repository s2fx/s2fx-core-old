using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model {
    public abstract class AbstractEntity : IEntity {
        public long Id { get; set; }

        public bool IsPersistent => Id <= 0;
    }
}
