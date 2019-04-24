using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Transactions {

    public interface ITransactionManager {

        ITransaction CurrentTransaction { get; }
        ITransaction BeginTransaction();

    }

}
