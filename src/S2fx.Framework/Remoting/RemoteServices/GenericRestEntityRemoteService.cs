using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using S2fx.Data;
using S2fx.Model;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using S2fx.Model.Metadata;

namespace S2fx.Remoting.RemoteServices {

    public class GenericRestEntityRemoteService<TEntity>
        where TEntity : class, IEntity {

        protected IRepository<TEntity> Repository { get; }
        protected IEntityManager EntityManager { get; }

        public ILogger Logger { get; set; }
        public MetaEntity Entity { get; }

        public GenericRestEntityRemoteService(
            IRepository<TEntity> repository,
            ILogger<GenericRestEntityRemoteService<TEntity>> logger,
            IEntityManager entityManager) {
            this.Repository = repository;
            this.Logger = logger;
            this.EntityManager = entityManager;

            this.Entity = entityManager.GetEntityByClrType(typeof(TEntity));
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: true)]
        public async Task<List<TEntity>> GetAllAsync() {
            return await this.Repository.GetAllPagedAsync(0, 50);
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get, isRestful: true)]
        public async Task<TEntity> Single([Url]long id) {
            //TODO with filter, selector ...
            return await this.Repository.SingleAsync(id);
        }

        [RemoteServiceMethod(httpMethod: HttpMethod.Get)]
        public async Task<EntityQueryResult> QueryAsync(
            string filter = null, string select = null, string sort = null, int offset = 0, int limit = 50) {
            //TODO with filter, selector ...
            var source = this.Repository.AllWithNoTrack();
            /*
            var filtered = table.OData()
                .Filter("Id gt 0")
                .SelectExpandAsQueryable("Id,UserName");
                */

            IQueryable filteredQuery = source;
            long total = 0;

            if (!string.IsNullOrEmpty(filter)) {
                var lambda = DynamicExpressionParser.ParseLambda(typeof(TEntity), typeof(bool), filter);
                var whereExpr = (Expression<Func<TEntity, bool>>)lambda;
                filteredQuery = source.Where(whereExpr);
                total = await Repository.CountAsync(whereExpr);
            }
            else {
                total = await Repository.CountAsync();
            }

            if (!string.IsNullOrEmpty(sort)) {
                filteredQuery = filteredQuery.OrderBy(sort);
            }

            if (offset >= 0) {
                filteredQuery = filteredQuery.Skip(offset);
            }

            if (limit > 0) {
                filteredQuery = filteredQuery.Take(limit);
            }

            if (!string.IsNullOrEmpty(select)) {
                filteredQuery = filteredQuery.Select(select);
            }

            var result = await this.Repository.ExecuteQueryAsync<IEnumerable<object>>(filteredQuery);
            return new EntityQueryResult {
                Entity = this.Entity.Name,
                Filter = filter,
                SortBy = sort,
                Select = select,
                Offset = offset,
                Limit = limit,
                Total = total,
                Count = result.Count(),
                Entities = result,
            };
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
