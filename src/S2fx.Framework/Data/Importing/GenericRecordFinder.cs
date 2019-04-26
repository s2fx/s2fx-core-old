using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Dynamic.Core;
using S2fx.Model;
using System.Linq.Expressions;
using S2fx.Utility;
using S2fx.Data.Importing.Model;

namespace S2fx.Data.Importing {

    public class GenericRecordFinder<TEntity> : IRecordFinder
        where TEntity : class, IEntity {

        private readonly ISafeRepository<TEntity> _repository;

        public GenericRecordFinder(ISafeRepository<TEntity> repository) {
            _repository = repository;
        }

        public async Task<object> FindExistedRecordOrDefaultAsync(ImportingTask importingTask, IReadOnlyDictionary<string, object> symbols) {
            var entityWhere = importingTask.Descriptor.ImportEntity.Where;
            if (string.IsNullOrEmpty(entityWhere)) {
                return null;
            }
            var pred = this.CreateEntityPredicateExpression(entityWhere, symbols);
            var repo = importingTask.Descriptor.IsSudo ? _repository.Sudo() : _repository;
            return await _repository.FirstOrDefaultAsync(pred);
        }

        private Expression<Func<TEntity, bool>> CreateEntityPredicateExpression(
            string expression, IReadOnlyDictionary<string, object> symbols) {
            var lambda = DynamicExpressionParser.ParseLambda(
                typeof(TEntity), typeof(bool), expression, symbols);
            var body = lambda as Expression<Func<TEntity, bool>>;
            return body;
        }

    }

}
