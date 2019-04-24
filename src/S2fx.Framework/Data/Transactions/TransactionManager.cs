using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Transactions {

    public class TransactionManager : ITransactionManager {

        readonly ITransactionFactory _transactionFactory;
        readonly ICurrentTransactionAccessor _currentTransactionAccessor;

        public TransactionManager(ITransactionFactory transactionFactory, ICurrentTransactionAccessor currentTransactionAccessor) {
            _transactionFactory = transactionFactory;
            _currentTransactionAccessor = currentTransactionAccessor;
        }

        public ITransaction CurrentTransaction => _currentTransactionAccessor.CurrentTransaction;

        public ITransaction BeginTransaction() {
            if (CurrentTransaction == null) {
                var tx = _transactionFactory.Create(null);
                _currentTransactionAccessor.CurrentTransaction = tx;
                return tx;
            }
            else {
                var tx = _transactionFactory.Create(_currentTransactionAccessor.CurrentTransaction);
                _currentTransactionAccessor.CurrentTransaction = tx;
                return tx;
            }
        }
    }

}
