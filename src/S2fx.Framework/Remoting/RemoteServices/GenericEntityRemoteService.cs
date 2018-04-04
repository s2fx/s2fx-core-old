using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using S2fx.Data;
using S2fx.Model;

namespace S2fx.Remoting.RemoteServices {

    public class GenericEntityRemoteService<TEntity>
        where TEntity : class, IEntity {

        protected IRepository<TEntity> Repository { get; }
        public ILogger Logger { get; set; }

        public GenericEntityRemoteService(
            IRepository<TEntity> repository,
            ILogger<GenericEntityRemoteService<TEntity>> logger) {
            this.Repository = repository;
            this.Logger = logger;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get)]
        public async Task<IEnumerable<TEntity>> All() {
            //TODO with filter, selector ...
            return await this.Repository.GetAllAsync();
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get)]
        public async Task<TEntity> Single(long id) {
            //TODO with filter, selector ...
            return await this.Repository.SingleAsync(id);
        }

    }

}
