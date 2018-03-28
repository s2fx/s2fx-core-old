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
            var dataSource = _dataSources.Single(x => x.Format == job.DataSource);

            using (var stream = job.ImportFileInfo.CreateReadStream()) {
                var rows = dataSource.GetAllRows(stream, job.EntityBinding.Selector);
                var recordImporterType = typeof(RecordImporter<>).MakeGenericType(context.Entity.ClrType);
                var recordImporter = (IRecordImporter)Activator.CreateInstance(recordImporterType, _services, job.EntityBinding.Where);

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

        private static async Task ImportSingleRecordAsync(
            ImportContext context, IRecordImporter recordImporter, object row) {

            var propValues = new Dictionary<string, object>(context.EntityBinding.Properties.Count());
            foreach (var propBind in context.EntityBinding.Properties) {

                var propertyValueExpression = propBind.SourceGetter(row);
                var metaProperty = context.Entity.Properties[propBind.TargetProperty];
                if (metaProperty.Type.TryParsePropertyValue(propertyValueExpression, out var propertyValue)) {
                    propValues.Add(metaProperty.Name, propertyValue);
                }
                else {
                    throw new DataImportingException(
                        $"Unable to parse the expression '{propertyValueExpression}' " +
                        "for property '{metaProperty.Entity.Name}#{metaProperty.Name}'");
                }
            }

            var symbols = propValues
                .ToDictionary(x => x.Key, x => x.Value);

            var existedRecord = await recordImporter.FindExistedRecordAsync(symbols);

            var needsImportRecord =
                (existedRecord == null)
                || (existedRecord != null && context.EntityBinding.CanUpdate);

            if (!needsImportRecord) {
                return;
            }

            var record = existedRecord ?? Activator.CreateInstance(context.Entity.ClrType);

            foreach (var propPair in propValues) {
                var metaProperty = context.Entity.Properties[propPair.Key];
                metaProperty.ClrPropertyInfo.SetValue(record, propPair.Value);
            }

            await recordImporter.InsertOrUpdateEntityAsync(record, context.EntityBinding.CanUpdate);
        }

        private ImportContext CreateImportContext(ImportDescriptor job) {
            var entity = _entityManager.GetEntity(job.Entity);
            var context = new ImportContext(job.Feature, entity, job.EntityBinding, null);

            //Populates property binders
            var ds = _dataSources.Single(x => x.Format == job.DataSource);

            foreach (var propertyBinding in job.EntityBinding.Properties) {
                propertyBinding.SourceGetter = ds.CreateInputPropertyValueTextGetter(propertyBinding.SourceExpression);
                //var binder = new PropertyBinder(sourceGetter, pbi.TargetProperty);
            }

            return context;
        }

        private object ParsePropertyValue(MetaProperty property, string value) {
            throw new NotImplementedException();
        }

    }
}
