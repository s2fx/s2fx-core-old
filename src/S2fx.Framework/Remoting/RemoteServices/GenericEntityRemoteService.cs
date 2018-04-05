using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using S2fx.Data;
using S2fx.Model;
using LinqToQuerystring.Utils;
using LinqToQuerystring;
using System.Linq;

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
        public object Query(EntityQueryParameters queryParams) {
            //TODO with filter, selector ...
            var table = this.Repository.All();
            var records = table.LinqToQuerystring(typeof(TEntity), queryParams.QueryString)
                as IEnumerable<object>;
            var result = new EntityQueryResult {
                Count = records.Count(),
                Value = records,
            };
            return result;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get)]
        public async Task<TEntity> Single(long id) {
            //TODO with filter, selector ...
            return await this.Repository.SingleAsync(id);
        }

    }

}
