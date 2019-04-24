using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using S2fx.Data.Transactions;

namespace S2fx.Data.NHibernate.Transactions {

    public class NHTransactionalSessionAccessor : INHSessionAccessor {
        readonly ICurrentTransactionAccessor _cta;
        public NHTransactionalSessionAccessor(ICurrentTransactionAccessor cta) {
            _cta = cta;
        }

        public ISession Session => ((NHTransaction)_cta.CurrentTransaction).NHSession;
    }

}
