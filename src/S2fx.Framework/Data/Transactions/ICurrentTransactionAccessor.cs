using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Transactions {

    public interface ICurrentTransactionAccessor {

        ITransaction CurrentTransaction { get; set; }
    }

}
