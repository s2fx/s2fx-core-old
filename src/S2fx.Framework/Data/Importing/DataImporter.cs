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

        public event EventHandler<EntityRecordImportedEventArgs> EntityRecordImported;

        public DataImporter(IServiceProvider services, IEntityManager entityManager, IEnumerable<IDataSource> dataSources) {
            _services = services;
            _entityManager = entityManager;
            _dataSources = dataSources;
        }

        public async Task ImportAsync(ImportDescriptor job) {
            var context = this.CreateImportContext(job);
            var dataSource = _dataSources.Single(x => x.Format == job.DataSource);

            using (var stream = job.ImportFileInfo.CreateReadStream()) {
                var rows = dataSource.GetAllRows(stream, job.EntityMapping.Selector);
                var recordFinderType = typeof(GenericRecordFinder<>).MakeGenericType(context.Entity.ClrType);
                var recordFinder = _services.GetRequiredService(recordFinderType) as IRecordFinder;

                var recordImporterType = typeof(GenericRecordImporter<>).MakeGenericType(context.Entity.ClrType);
                var recordImporter = _services.GetRequiredService(recordImporterType) as IRecordImporter;

                foreach (var row in rows) {
                    await ImportSingleRecordAsync(context, recordFinder, recordImporter, row);
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

        private async Task ImportSingleRecordAsync(
            ImportContext context, IRecordFinder recordFinder, IRecordImporter recordImporter, object row) {

            var propValues = new Dictionary<string, object>(context.EntityBinding.PropertyMappings.Count());
            foreach (var propBind in context.EntityBinding.PropertyMappings) {

                var propertyValueExpression = propBind.SourceGetter(row);
                var metaProperty = context.Entity.Fields[propBind.TargetProperty];
                if (metaProperty.Type.TryParse(metaProperty, propertyValueExpression, out var propertyValue, propBind.Format)) {
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

            var existedRecord = await recordFinder.FindExistedRecordOrDefaultAsync(context, symbols);

            var needsImportRecord =
                (existedRecord == null)
                || (existedRecord != null && context.EntityBinding.CanUpdate);

            if (!needsImportRecord) {
                return;
            }

            var record = existedRecord ?? Activator.CreateInstance(context.Entity.ClrType);

            foreach (var propPair in propValues) {
                var metaProperty = context.Entity.Fields[propPair.Key];
                metaProperty.ClrPropertyInfo.SetValue(record, propPair.Value);
            }

            await recordImporter.InsertOrUpdateEntityAsync(context, record, context.EntityBinding.CanUpdate);
            this.EntityRecordImported?.Invoke(this, new EntityRecordImportedEventArgs(context.Entity, record));
        }

        private ImportContext CreateImportContext(ImportDescriptor job) {
            var entity = _entityManager.GetEntity(job.Entity);
            var context = new ImportContext(job.Feature, entity, job.EntityMapping, null);

            //Populates property binders
            var ds = _dataSources.SingleOrDefault(x => x.Format == job.DataSource);
            if (ds == null) {
                var msg = string.Format("Unsupported format of seeding data: '{0}'", job.DataSource);
                throw new NotSupportedException(msg);
            }

            foreach (var propertyBinding in job.EntityMapping.PropertyMappings) {
                propertyBinding.SourceGetter = ds.CreateInputPropertyValueTextGetter(propertyBinding.SourceExpression);
            }

            return context;
        }

        private object ParsePropertyValue(MetaField property, string value) {
            //TODO parse property value
            throw new NotImplementedException();
        }

    }
}
