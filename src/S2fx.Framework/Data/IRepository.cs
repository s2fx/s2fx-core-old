using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using S2fx.Model;

namespace S2fx.Data {

    public interface IRepository {
    }

    public interface IRepository<TEntity> : IRepository
        where TEntity : class, IEntity {

        IQueryable<TEntity> All();

        IQueryable<TEntity> AllWithNoTrack();

        Task<TResult> ExecuteQueryAsync<TResult>(IQueryable queryable, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> GetAllPagedAsync(int offset = 0, int limit = 50);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleAsync(long id);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(long id);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<long> InsertAndGetIdAsync(TEntity entity);

        Task<TEntity> InsertOrUpdateAsync(TEntity entity);

        Task<long> InsertOrUpdateAndGetIdAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteSingleAsync(TEntity entity);

        Task DeleteSingleAsync(long id);

        Task DeleteManyAsync(IEnumerable<long> ids);

        Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate);

        Task<long> CountAsync();

        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }

}
