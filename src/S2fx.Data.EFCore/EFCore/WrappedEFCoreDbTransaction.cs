using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.EFCore {

    public sealed class WrappedEFCoreDbTransaction : IDbTransaction {

        Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction EFCoreDbTransaction { get; }

        public Guid TransactionId => this.EFCoreDbTransaction.TransactionId;

        public WrappedEFCoreDbTransaction(Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction eftx) {
            this.EFCoreDbTransaction = eftx;
        }

        public void Commit() {
            this.EFCoreDbTransaction.Commit();
        }

        public void Rollback() {
            this.EFCoreDbTransaction.Rollback();
        }
    }

}
