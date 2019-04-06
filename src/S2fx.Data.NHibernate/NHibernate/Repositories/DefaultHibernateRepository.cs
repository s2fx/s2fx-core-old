using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using S2fx.Model;

namespace S2fx.Data.NHibernate {

    public class DefaultHibernateRepository<TEntity> : IHibernateRepository<TEntity>
        where TEntity : class, IEntity {

        public ISession DbSession { get; }

        public IQueryable<TEntity> Table => this.DbSession.Query<TEntity>();

        public DefaultHibernateRepository(ISession dbSession) {
            this.DbSession = dbSession ?? throw new ArgumentNullException(nameof(dbSession));
        }

        public IQueryable<TEntity> All() => this.Table;

        public IQueryable<TEntity> AllWithNoTrack() {
            return this.Table.WithOptions(x => {
                x.SetCacheable(false);
                x.SetCacheMode(CacheMode.Ignore);
            });
        }

        public Task<TResult> ExecuteQueryAsync<TResult>(IQueryable source, CancellationToken cancellationToken = default) {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }
            if (!(source.Provider is INhQueryProvider provider)) {
                throw new NotSupportedException($"Source {nameof(source.Provider)} must be a {nameof(INhQueryProvider)}");
            }
            if (cancellationToken.IsCancellationRequested) {
                return Task.FromCanceled<TResult>(cancellationToken);
            }
            return InternalToListAsync();

            async Task<TResult> InternalToListAsync() {
                return await provider.ExecuteAsync<TResult>(source.Expression, cancellationToken).ConfigureAwait(false);
            }
        }

        public Task<List<TEntity>> GetAllAsync() =>
            this.Table.ToListAsync();

        public Task<List<TEntity>> GetAllPagedAsync(int offset = 0, int limit = 50) =>
            this.Table.Skip(offset).Take(limit).ToListAsync();

        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate) =>
            this.Table.Where(predicate).ToListAsync();

        public Task<TEntity> SingleAsync(long id) =>
            this.DbSession.LoadAsync<TEntity>(id);

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate) =>
            this.Table.SingleAsync(predicate);

        public Task<TEntity> FirstOrDefaultAsync(long id) =>
            this.DbSession.GetAsync<TEntity>(id);

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) =>
            this.Table.FirstOrDefaultAsync(predicate);

        public async Task<TEntity> InsertAsync(TEntity entity) =>
            (await this.DbSession.SaveAsync(entity)) as TEntity;

        public async Task<long> InsertAndGetIdAsync(TEntity entity) {
            if (entity.IsPersistent) {
                await this.DbSession.SaveOrUpdateAsync(entity);
                return entity.Id;
            }
            else {
                var savedEntity = this.DbSession.SaveAsync(entity);
                return savedEntity.Id;
            }
        }

        public async Task<TEntity> InsertOrUpdateAsync(TEntity entity) =>
            entity.IsPersistent ? await this.UpdateAsync(entity) : await this.InsertAsync(entity);

        public async Task<long> InsertOrUpdateAndGetIdAsync(TEntity entity) {
            if (entity.IsPersistent) {
                await this.DbSession.SaveOrUpdateAsync(entity);
                return entity.Id;
            }
            else {
                var savedEntity = this.DbSession.SaveAsync(entity);
                return savedEntity.Id;
            }
        }

        public Task<TEntity> UpdateAsync(TEntity entity) {
            if (!entity.IsPersistent) {
                throw new InvalidOperationException();
            }
            return Task.FromResult(entity);
        }

        public async Task DeleteSingleAsync(TEntity entity) {
            if (!entity.IsPersistent) {
                throw new InvalidOperationException();
            }

            await this.Table.Where(e => e.Id == entity.Id).DeleteAsync(default(CancellationToken));
        }

        public async Task DeleteSingleAsync(long id) {
            var entity = await this.FirstOrDefaultAsync(id);
            if (entity != default(TEntity)) {
                await this.DeleteSingleAsync(entity);
            }
        }

        public Task DeleteManyAsync(IEnumerable<long> ids) {
            throw new NotImplementedException();
        }

        public Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate) {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync() =>
            this.Table.LongCountAsync();

        public Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate) =>
            this.Table.LongCountAsync(predicate);

      
    }
}
