using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Model {

    public interface IEntity {
        long Id { get; set; }
        bool IsPersistent { get; }
    }

}
