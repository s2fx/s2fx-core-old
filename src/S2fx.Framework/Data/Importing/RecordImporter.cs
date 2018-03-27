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

namespace S2fx.Data.Importing {

    public interface IRecordImporter {

        Task InsertOrUpdateEntityAsync(object record, bool canUpdate);
        Task<object> FindExistedRecordAsync(IDictionary<string, object> symbols);
    }

    public class RecordImporter<TEntity> : IRecordImporter
        where TEntity : class, IEntity {

        private readonly IServiceProvider _services;
        private readonly IRepository<TEntity> _repository;
        private readonly string _entitywhere;

        public RecordImporter(IServiceProvider services, string entityWhere) {
            _services = services;
            _repository = services.GetRequiredService<IRepository<TEntity>>();
            _entitywhere = entityWhere;
        }

        public async Task InsertOrUpdateEntityAsync(object record, bool canUpdate) {
            var typedRecord = (TEntity)record;
            await _repository.InsertOrUpdateAsync(typedRecord);
        }

        public async Task<object> FindExistedRecordAsync(IDictionary<string, object> symbols) {
            if (string.IsNullOrEmpty(_entitywhere)) {
                return null;
            }
            var pred = this.CreateEntityPredicateExpression(_entitywhere, symbols);
            return await _repository.FirstOrDefaultAsync(pred);
        }

        private Expression<Func<TEntity, bool>> CreateEntityPredicateExpression(
            string expression, IDictionary<string, object> symbols) {
            var lambda = DynamicExpressionParser.ParseLambda(
                typeof(TEntity), typeof(bool), _entitywhere, symbols);
            var body = lambda as Expression<Func<TEntity, bool>>;
            return body;
        }

    }

}
