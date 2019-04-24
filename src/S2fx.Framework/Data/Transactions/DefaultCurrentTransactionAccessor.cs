using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace S2fx.Data.Transactions {

    public class DefaultCurrentTransactionAccessor : ICurrentTransactionAccessor {
        private static AsyncLocal<TransactionHolder> s_currentTransaction = new AsyncLocal<TransactionHolder>();

        public ITransaction CurrentTransaction {
            get => s_currentTransaction.Value?.Transaction;
            set {
                var holder = s_currentTransaction.Value;
                if (holder != null) {
                    // Clear current transaction trapped in the AsyncLocals, as its done.
                    holder.Transaction = null;
                }

                if (value != null) {
                    // Use an object indirection to hold the HttpContext in the AsyncLocal,
                    // so it can be cleared in all ExecutionContexts when its cleared.
                    s_currentTransaction.Value = new TransactionHolder(value);
                }
            }
        }

        class TransactionHolder {
            public TransactionHolder(ITransaction tx) => this.Transaction = tx;
            public ITransaction Transaction { get; set; }
        }
    }

}
