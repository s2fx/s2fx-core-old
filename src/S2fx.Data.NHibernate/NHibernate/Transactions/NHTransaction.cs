using System;
using System.Collections.Generic;
using System.Text;
using NH = NHibernate;
using S2fx.Data.Transactions;
using System.Threading.Tasks;

namespace S2fx.Data.NHibernate.Transactions {

    public class NHTransaction : ITransaction {

        public NH.ISession NHSession { get; private set; }

        public NH.ITransaction NHDbTransaction { get; private set; }

        public Guid Id { get; } = new Guid();

        public ITransaction Parent { get; }

        public NHTransaction(NH.ISession nhSession, ITransaction parent = null) {
            this.NHSession = nhSession;
            this.NHDbTransaction = this.NHSession.BeginTransaction();
        }

        public ITransaction BeginNested() {
            return new NHTransaction(this.NHSession, this);
        }

        public void Commit() {
            this.NHDbTransaction.Commit();
        }

        public async Task CommitAsync() {
            await this.NHDbTransaction.CommitAsync();
        }

        public void Dispose() {
            this.NHDbTransaction.Dispose();
        }
    }

}
