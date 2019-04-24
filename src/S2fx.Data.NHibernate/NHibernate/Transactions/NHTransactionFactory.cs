using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NH = NHibernate;
using S2fx.Data.Transactions;

namespace S2fx.Data.NHibernate.Transactions {

    public class NHTransactionFactory : ITransactionFactory {
        readonly NH.ISession _session;

        public NHTransactionFactory(NH.ISession session) {
            _session = session;
        }

        public ITransaction Create(ITransaction parent) {
            return new NHTransaction(_session, parent);
        }

    }

}
