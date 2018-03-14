using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Data.UnitOfWork {

    public interface IUnitOfWork : IDisposable {
        Guid Id { get; }
        TimeSpan Timeout { get; }
        bool IsTransactional { get; }
        IUnitOfWork Parent { get; }
        IDbTransaction Transaction { get; }
        UnitOfWorkStatus State { get; }

        Task BeginAsync(UnitOfWorkOptions options);
        Task BeginAsync();
        Task SaveChangesAsync();
        Task CompleteAsync();
    }

}
