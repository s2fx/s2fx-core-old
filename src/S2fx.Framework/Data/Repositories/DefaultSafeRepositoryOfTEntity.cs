using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using S2fx.Model;

namespace S2fx.Data.Repositories {

    public class DefaultSafeRepository<TEntity> : ISafeRepository<TEntity> where TEntity : class, IEntity {
        readonly IRepository<TEntity> _unsafeRepo;

        public DefaultSafeRepository(IRepository<TEntity> unsafeRepo) {
            _unsafeRepo = unsafeRepo;
        }

        public IRepository<TEntity> Sudo() => _unsafeRepo;

        public IQueryable<TEntity> All() => _unsafeRepo.All();

        public IQueryable<TEntity> AllWithNoTrack() => _unsafeRepo.AllWithNoTrack();

        public Task<TResult> ExecuteQueryAsync<TResult>(IQueryable source, CancellationToken cancellationToken = default) {
            return _unsafeRepo.ExecuteQueryAsync<TResult>(source, cancellationToken);
        }

        public Task<List<TEntity>> GetAllAsync() => _unsafeRepo.GetAllAsync();

        public Task<List<TEntity>> GetAllPagedAsync(int offset = 0, int limit = 50) => _unsafeRepo.GetAllPagedAsync();

        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate) => _unsafeRepo.GetAllAsync(predicate);

        public Task<TEntity> SingleAsync(long id) => _unsafeRepo.SingleAsync(id);

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate) => _unsafeRepo.SingleAsync(predicate);

        public Task<TEntity> FirstOrDefaultAsync(long id) => _unsafeRepo.FirstOrDefaultAsync(id);

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => _unsafeRepo.FirstOrDefaultAsync(predicate);

        public Task<TEntity> InsertAsync(TEntity entity) => _unsafeRepo.InsertAsync(entity);

        public Task<long> InsertAndGetIdAsync(TEntity entity) => _unsafeRepo.InsertAndGetIdAsync(entity);

        public Task<TEntity> InsertOrUpdateAsync(TEntity entity) => _unsafeRepo.InsertOrUpdateAsync(entity);

        public Task<long> InsertOrUpdateAndGetIdAsync(TEntity entity) => _unsafeRepo.InsertOrUpdateAndGetIdAsync(entity);

        public Task<TEntity> UpdateAsync(TEntity entity) => _unsafeRepo.UpdateAsync(entity);

        public Task DeleteSingleAsync(TEntity entity) => _unsafeRepo.DeleteSingleAsync(entity);

        public Task DeleteSingleAsync(long id) => _unsafeRepo.DeleteSingleAsync(id);

        public Task DeleteManyAsync(IEnumerable<long> ids) => _unsafeRepo.DeleteManyAsync(ids);

        public Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate) => throw new NotImplementedException();

        public Task<long> CountAsync() => _unsafeRepo.CountAsync();

        public Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate) => _unsafeRepo.CountAsync(predicate);

    }

}
