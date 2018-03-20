using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using S2fx.Data.UnitOfWork;

namespace S2fx.Data.EFCore.UnitOfWork {

    public class EFCoreUnitOfWork : AbstractUnitOfWork {

        private readonly DbContext _dbContext;
        private WrappedEFCoreDbTransaction _wrappedTransaction;

        public DbContext DbContext { get; }

        public override IDbConnection DbConnection => _dbContext.Database.GetDbConnection();

        public override IDbTransaction DbTransaction => _wrappedTransaction;

        public EFCoreUnitOfWork(DbContext dbContext) {
            _dbContext = dbContext;
        }

        protected override void BeginUow() {
            if (this.IsTransactional) {
                try {
                    var dbtx = _dbContext.Database.BeginTransaction();
                    _wrappedTransaction = new WrappedEFCoreDbTransaction(dbtx);
                }
                catch {
                    _wrappedTransaction = null;
                }
            }
        }

        public override async Task CompleteAsync() {
            if (this.State != UnitOfWorkStatus.Started) {
                throw new InvalidOperationException();
            }

            try {
                await this.SaveChangesAsync();
                if (this.DbTransaction != null) {
                    this.DbTransaction.Commit();
                }
            }
            catch {
                this.State = UnitOfWorkStatus.Failed;
            }

            this.State = UnitOfWorkStatus.Succeed;
        }

        public override async Task SaveChangesAsync() {
            if (this.State != UnitOfWorkStatus.Started) {
                throw new InvalidOperationException();
            }

            await this.DbContext.SaveChangesAsync();
        }

        public override void Dispose() {
            if (this.State == UnitOfWorkStatus.Started) {
                //DO disposing
            }
        }

    }
}
