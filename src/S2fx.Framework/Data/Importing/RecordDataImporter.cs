using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Model;

namespace S2fx.Data.Importing {

    public interface IRecordDataImporter {

        Task ImportAsync(object record, bool canUpdate);
    }

    public class RecordDataImporter<TEntity> : IRecordDataImporter
        where TEntity : class, IEntity {
        private IServiceProvider _services;
        private IRepository<TEntity> _repository;

        public RecordDataImporter(IServiceProvider services) {
            _services = services;
            _repository = services.GetRequiredService<IRepository<TEntity>>();
        }

        public async Task ImportAsync(object record, bool canUpdate) {
            var typedRecord = (TEntity)record;
            await _repository.InsertAsync(typedRecord);
        }

    }

}
