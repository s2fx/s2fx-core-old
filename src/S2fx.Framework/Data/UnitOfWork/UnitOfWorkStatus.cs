using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.UnitOfWork {

    public enum UnitOfWorkStatus {
        NotStarted,
        Started,
        Failed,
        Succeed
    }
}
