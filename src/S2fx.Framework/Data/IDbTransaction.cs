using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data {

    public interface IDbTransaction {
        Guid TransactionId { get; }

        void Commit();

        void Rollback();
    }
}
