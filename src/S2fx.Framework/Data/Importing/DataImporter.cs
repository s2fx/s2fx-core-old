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
using OrchardCore.DeferredTasks;

namespace S2fx.Data.Importing {

    public class DataImporter : IDataImporter {

        readonly IServiceProvider _services;
        readonly IDeferredTaskEngine _defferedTaskEngine;
        readonly IEntityManager _entityManager;
        readonly IEnumerable<IDataSource> _dataSources;

        public event EventHandler<EntityRecordImportedEventArgs> EntityRecordImported;

        public DataImporter(
            IServiceProvider services,
            IDeferredTaskEngine defferedTaskEngine,
            IEntityManager entityManager, IEnumerable<IDataSource> dataSources) {
            _services = services;
            _defferedTaskEngine = defferedTaskEngine;
            _entityManager = entityManager;
            _dataSources = dataSources;
        }

        public async Task ImportAsync(ImportingTaskDescriptor descriptor) {
            _defferedTaskEngine.AddTask(async defferedTaskContext => {
                await this.DoImportAsync(descriptor);
            });
            await Task.CompletedTask;
        }

        public async Task ImportAsync(IEnumerable<ImportingTaskDescriptor> sortedDescriptors) {
            _defferedTaskEngine.AddTask(async defferedTaskContext => {
                foreach (var descriptor in sortedDescriptors) {
                    await this.DoImportAsync(descriptor);
                }
            });
            await Task.CompletedTask;
        }

        async Task DoImportAsync(ImportingTaskDescriptor descriptor) {
            var context = this.CreateImportContext(descriptor);
            var dataSource = _dataSources.Single(x => x.Format == descriptor.DataSource);

            using (var stream = descriptor.ImportFileInfo.CreateReadStream()) {
                var recordFinderType = typeof(GenericRecordFinder<>).MakeGenericType(context.Entity.ClrType);
                var recordFinder = _services.GetRequiredService(recordFinderType) as IRecordFinder;

                var recordImporterType = typeof(GenericRecordImporter<>).MakeGenericType(context.Entity.ClrType);
                var recordImporter = _services.GetRequiredService(recordImporterType) as IRecordImporter;

                var reader = dataSource.Open(stream); //GetAllRows(stream, descriptor.EntityMapping.Selector);
                while (await reader.ReadAsync()) {
                    await ImportSingleRecordAsync(context, recordFinder, recordImporter, reader);
                }
            }
        }

        async Task ImportSingleRecordAsync(
            ImportingTaskContext context, IRecordFinder recordFinder, IRecordImporter recordImporter, IDataSourceReader reader) {

            var propValues = new Dictionary<string, object>(context.EntityBinding.PropertyMappings.Length);
            foreach (var propBind in context.EntityBinding.PropertyMappings) {
                var propertyValueExpression = reader.GetField(propBind.SourceExpression).ToString();
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

        ImportingTaskContext CreateImportContext(ImportingTaskDescriptor job) {
            var entity = _entityManager.GetEntity(job.Entity);
            var context = new ImportingTaskContext(job.Feature, entity, job.EntityMapping, null);

            //Populates property binders
            var ds = _dataSources.SingleOrDefault(x => x.Format == job.DataSource);
            if (ds == null) {
                var msg = string.Format("Unsupported format of seeding data: '{0}'", job.DataSource);
                throw new NotSupportedException(msg);
            }

            /*
            foreach (var propertyBinding in job.EntityMapping.PropertyMappings) {
                propertyBinding.SourceGetter = ds.CreateInputPropertyValueTextGetter(propertyBinding.SourceExpression);
            }
            */

            return context;
        }

        object ParsePropertyValue(MetaField property, string value) {
            //TODO parse property value
            throw new NotImplementedException();
        }

    }
}
