using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Model;
using System.Linq.Expressions;
using S2fx.Utility;
using S2fx.Data.Importing.Model;

namespace S2fx.Data.Importing {

    public class GenericRecordImporter<TEntity> : IRecordImporter
        where TEntity : class, IEntity {

        private readonly IRepository<TEntity> _repository;

        public GenericRecordImporter(IRepository<TEntity> repository) {
            _repository = repository;
        }

        public async Task InsertOrUpdateEntityAsync(ImportContext context, object record, bool canUpdate) {
            var typedRecord = (TEntity)record;
            await _repository.InsertOrUpdateAsync(typedRecord);
        }
    }

}
