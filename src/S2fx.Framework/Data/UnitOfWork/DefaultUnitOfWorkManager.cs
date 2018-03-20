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

        public IUnitOfWork Begin(UnitOfWorkOptions options) {
            var uow = _serviceProvider.GetService<IUnitOfWork>();
            uow.Begin(options);
            return uow;
        }

        public IUnitOfWork Begin() {
            var uow = _serviceProvider.GetService<IUnitOfWork>();
            uow.Begin(UnitOfWorkOptions.DefaultOptions);
            return uow;
        }
    }
}
