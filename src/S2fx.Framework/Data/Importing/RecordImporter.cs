using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Dynamic.Core;
using S2fx.Model;
using LinqToQuerystring;
using System.Linq.Expressions;
using S2fx.Utility;
using S2fx.Data.Importing.Model;

namespace S2fx.Data.Importing {

    public interface IRecordImporter {

        Task InsertOrUpdateEntityAsync(ImportContext context, object record, bool canUpdate);
        Task<object> FindExistedRecordAsync(ImportContext context, IDictionary<string, object> symbols);
    }

    public class RecordImporter<TEntity> : IRecordImporter
        where TEntity : class, IEntity {

        private readonly IRepository<TEntity> _repository;

        public RecordImporter(IRepository<TEntity> repository) {
            _repository = repository;
        }

        public async Task InsertOrUpdateEntityAsync(ImportContext context, object record, bool canUpdate) {
            var typedRecord = (TEntity)record;
            await _repository.InsertOrUpdateAsync(typedRecord);
        }

        public async Task<object> FindExistedRecordAsync(ImportContext context, IDictionary<string, object> symbols) {
            if (string.IsNullOrEmpty(context.EntityBinding.Where)) {
                return null;
            }
            var pred = this.CreateEntityPredicateExpression(context.EntityBinding.Where, symbols);
            return await _repository.FirstOrDefaultAsync(pred);
        }

        private Expression<Func<TEntity, bool>> CreateEntityPredicateExpression( 
            string expression, IDictionary<string, object> symbols) {
            var lambda = DynamicExpressionParser.ParseLambda(
                typeof(TEntity), typeof(bool), expression, symbols);
            var body = lambda as Expression<Func<TEntity, bool>>;
            return body;
        }

    }

}
