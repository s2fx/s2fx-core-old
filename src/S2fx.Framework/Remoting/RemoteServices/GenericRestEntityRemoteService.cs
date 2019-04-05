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

    public class GenericRestEntityRemoteService<TEntity>
        where TEntity : class, IEntity {

        protected IRepository<TEntity> Repository { get; }
        public ILogger Logger { get; set; }

        public GenericRestEntityRemoteService(
            IRepository<TEntity> repository,
            ILogger<GenericRestEntityRemoteService<TEntity>> logger) {
            this.Repository = repository;
            this.Logger = logger;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: true)]
        public object GetAll() {
            var results = this.Repository.All().Take(50).ToArray();
            return results;
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: true)]
        public async Task<TEntity> Single([Url]long id) {
            //TODO with filter, selector ...
            return await this.Repository.SingleAsync(id);
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get)]
        public object Query([Body]EntityQueryParameters queryParams) {
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

        [RemoteServiceMethod(httpMethod: HttpMethod.Delete, isRestful: true)]
        public async Task Delete([Url]long id) {
            //TODO with filter, selector ...
            await this.Repository.DeleteSingleAsync(id);
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Post)]
        public async Task DeleteMany([Body]IEnumerable<long> ids) {
            //TODO with filter, selector ...
            await this.Repository.DeleteManyAsync(ids);
        }


    }

}
