using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Transaction;
using S2fx.Data.UnitOfWork;

namespace S2fx.Data.NHibernate.UnitOfWork {

    public class HibernateUnitOfWork : AbstractUnitOfWork {

        private readonly ISessionFactory _sessionFactory;
        private ISession _dbSession;
        private ITransaction _nhtransaction;

        public ISession DbSession { get; }

        public override IDbConnection DbConnection => _dbSession.Connection;

        public override IDbTransaction DbTransaction {
            get {
                //a quick and dirty hack from ASP.NET Boilerplate project
                var adoTransaction = _nhtransaction as AdoTransaction;
                return GetFieldValue(typeof(AdoTransaction), adoTransaction, "trans") as IDbTransaction;
            }
        }

        public HibernateUnitOfWork(ISessionFactory sessionFactory) {
            _sessionFactory = sessionFactory;
        }

        protected override void BeginUow() {
            if (this._dbSession != null) {
                throw new InvalidOperationException();
            }

            this._dbSession = _sessionFactory.OpenSession();

            if (this.IsTransactional) {
                try {
                    _nhtransaction = _dbSession.BeginTransaction();
                }
                catch {
                    _nhtransaction = null;
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

            await this._dbSession.FlushAsync();
        }

        public override void Dispose() {
            if (this.State == UnitOfWorkStatus.Started) {
                //DO disposing
            }
        }

        private static object GetFieldValue(Type type, object instance, string fieldName) {
            return type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(instance);
        }

    }
}
