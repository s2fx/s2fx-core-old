using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NH = NHibernate;
using S2fx.Data.Transactions;

namespace S2fx.Data.NHibernate.Transactions {

    public class NHTransactionFactory : ITransactionFactory {
        readonly NH.ISessionFactory _sessionFactory;

        public NHTransactionFactory(NH.ISessionFactory sessionFactory) {
            _sessionFactory = sessionFactory;
        }

        public ITransaction Create(ITransaction parent) {
            var session = _sessionFactory.OpenSession();
            return new NHTransaction(session, parent);
        }

    }

}
