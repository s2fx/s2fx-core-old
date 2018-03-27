using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Data.Importing.Model;
using S2fx.Model;
using S2fx.Model.Metadata;

namespace S2fx.Data.Importing {

    public class DataImporter : IDataImporter {

        private readonly IServiceProvider _services;
        private readonly IEntityManager _entityManager;
        private readonly IEnumerable<IDataSource> _dataSources;

        public DataImporter(IServiceProvider services, IEntityManager entityManager, IEnumerable<IDataSource> dataSources) {
            _services = services;
            _entityManager = entityManager;
            _dataSources = dataSources;
        }

        public async Task ImportAsync(ImportDescriptor job) {
            var context = this.CreateImportContext(job);
            var dataSource = _dataSources.Single(x => x.Format == job.Format);

            using (var stream = job.ImportFileInfo.CreateReadStream()) {
                var rows = dataSource.GetAllRows(stream, job.Selector);
                var recordImporterType = typeof(RecordDataImporter<>).MakeGenericType(context.Entity.ClrType);
                var recordImporter = Activator.CreateInstance(recordImporterType, _services) as IRecordDataImporter;

                foreach (var row in rows) {
                    await ImportSingleRecordAsync(context, recordImporter, row);
                }
            }
        }

        public async Task ImportAsync(IEnumerable<ImportDescriptor> jobs) {
            //TODO dependency sort
            var sortedJobs = jobs;

            foreach (var job in jobs) {
                await this.ImportAsync(job);
            }
        }

        private static async Task ImportSingleRecordAsync(ImportContext context, IRecordDataImporter recordImporter, object row) {
            var record = Activator.CreateInstance(context.Entity.ClrType);
            foreach (var propBind in context.PropertyBinders) {

                var propertyValue = propBind.SourceGetter(row);
                var propertyInfo = context.Entity.Properties[propBind.TargetProperty];
                //set the property 
                //TODO
                propertyInfo.ClrPropertyInfo.SetValue(record, propertyValue);
            }
            await recordImporter.ImportAsync(record, context.CanUpdate);
        }

        private ImportContext CreateImportContext(ImportDescriptor job) {
            var entity = _entityManager.GetEntity(job.Entity);
            var context = new ImportContext(job.Feature, entity, job.CanUpdate, null);

            //Populates property binders
            var ds = _dataSources.Single(x => x.Format == job.Format);

            foreach (var pbi in job.PropertyBindingInfos) {
                var sourceGetter = ds.BindInputPropertyGetter(pbi.SourceExpression);
                var binder = new PropertyBinder(sourceGetter, pbi.TargetProperty);
                context.PropertyBinders.Add(binder);
            }

            return context;
        }

        private object ParsePropertyValue(MetaProperty property, string value) {
            throw new NotImplementedException();
        }

    }
}
