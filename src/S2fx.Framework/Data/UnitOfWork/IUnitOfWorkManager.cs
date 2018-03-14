using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S2fx.Data.UnitOfWork {

    public interface IUnitOfWorkManager {
        IUnitOfWork Current { get; }

        Task<IUnitOfWork> BeginAsync(UnitOfWorkOptions options);

        Task<IUnitOfWork> BeginAsync();
    }
}
