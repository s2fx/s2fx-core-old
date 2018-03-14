using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using S2fx.Model;

namespace S2fx.Data.EFCore {

    public class DefaultEFCoreRepository<TEntity> : IEFRepository<TEntity>
        where TEntity : class, IEntity {

        public DbContext DbContext { get; }

        public DbSet<TEntity> Table => this.DbContext.Set<TEntity>();

        public DefaultEFCoreRepository(DbContext dbContext) {
            this.DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IQueryable<TEntity> All() => this.Table;

        public Task<TEntity[]> GetAllAsync() =>
            this.DbContext.Set<TEntity>().ToArrayAsync();

        public Task<TEntity[]> GetAllAsync(Expression<Func<TEntity, bool>> predicate) =>
            this.Table.Where(predicate).ToArrayAsync();

        public Task<TEntity> SingleAsync(long id) =>
            this.Table.SingleAsync(e => e.Id == id);

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate) =>
            this.Table.SingleAsync(predicate);

        public Task<TEntity> FirstOrDefaultAsync(long id) =>
            this.Table.FirstOrDefaultAsync(e => e.Id == id);

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) =>
            this.Table.FirstOrDefaultAsync(predicate);

        public async Task<TEntity> InsertAsync(TEntity entity) {
            var entry = await this.Table.AddAsync(entity);
            return entry.Entity;
        }

        public async Task<long> InsertAndGetIdAsync(TEntity entity) {
            var insertedEntity = await this.InsertAsync(entity);
            if (!insertedEntity.IsPersistent) {
                await this.DbContext.SaveChangesAsync();
            }
            return insertedEntity.Id;
        }

        public async Task<TEntity> InsertOrUpdateAsync(TEntity entity) =>
            entity.IsPersistent ? await this.UpdateAsync(entity) : await this.InsertAsync(entity);

        public async Task<long> InsertOrUpdateAndGetIdAsync(TEntity entity) {
            var presistentedEntity = await this.InsertOrUpdateAsync(entity);
            await this.DbContext.SaveChangesAsync();
            return presistentedEntity.Id;
        }

        public Task<TEntity> UpdateAsync(TEntity entity) {
            if (!entity.IsPersistent) {
                throw new InvalidOperationException();
            }

            if (!this.DbContext.ChangeTracker.Entries<TEntity>().Any(e => e.Entity == entity)) {
                Table.Attach(entity);
            }

            this.DbContext.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        public Task DeleteSingleAsync(TEntity entity) {
            if (!entity.IsPersistent) {
                throw new InvalidOperationException();
            }

            this.DbContext.Remove(entity);
            return Task.FromResult(entity);
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
    }
}
