using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Data.Importing.Model;
using S2fx.Model;

namespace S2fx.Data.Importing {

    public class DataImporter : IDataImporter {

        private readonly IServiceProvider _services;

        public DataImporter(IServiceProvider services) {
            _services = services;
        }

        public async Task ImportAsync(ImportJob job) {
            var dataSources = _services.GetServices<IDataSource>();
            var dataSource = dataSources.Single(x => x.Kind == job.DataSource.Kind);
            var rows = dataSource.GetAllRows(job.DataSource);
            var recordImporterType = typeof(RecordDataImporter<>).MakeGenericType(job.Entity.ClrType);
            var recordImporter = Activator.CreateInstance(recordImporterType, _services) as IRecordDataImporter;

            foreach (var row in rows) {
                foreach (var propBind in job.Bind.Properties) {
                    var record = Activator.CreateInstance(job.Entity.ClrType);

                    var propertyValue = propBind.SourceGetter(row);
                    var propertyInfo = job.Entity.Properties[propBind.TargetProperty];
                    //set the property 
                    //TODO
                    propertyInfo.ClrPropertyInfo.SetValue(record, propertyValue);
                    await recordImporter.ImportAsync(record, job.CanUpdate);
                }
            }
        }

        public async Task ImportAsync(IEnumerable<ImportJob> jobs) {
            //TODO dependency sort
            var sortedJobs = jobs;

            foreach (var job in jobs) {
                await this.ImportAsync(job);
            }
        }
    }
}
