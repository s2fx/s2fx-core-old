using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Data.Transactions {

    public interface ITransaction : IDisposable {
        Guid Id { get; }

        ITransaction Parent { get; }

        ITransaction BeginNested();

        Task CommitAsync();
        void Commit();
    }

}
