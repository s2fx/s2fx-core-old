using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace S2fx.Data.UnitOfWork {

    public class DefaultUnitOfWorkManager : IUnitOfWorkManager {
        private readonly IServiceProvider _serviceProvider;

        public IUnitOfWork Current => throw new NotImplementedException();

        public DefaultUnitOfWorkManager(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public async Task<IUnitOfWork> BeginAsync(UnitOfWorkOptions options) {
            var uow = _serviceProvider.GetService<IUnitOfWork>();
            await uow.BeginAsync(options);
            return uow;
        }

        public async Task<IUnitOfWork> BeginAsync() {
            var uow = _serviceProvider.GetService<IUnitOfWork>();
            await uow.BeginAsync(UnitOfWorkOptions.DefaultOptions);
            return uow;
        }
    }
}
