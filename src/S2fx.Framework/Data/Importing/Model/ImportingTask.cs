using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileProviders;
using OrchardCore.Environment.Extensions.Features;
using S2fx.Model.Metadata;

namespace S2fx.Data.Importing.Model {

    public class ImportingTask {
        public Guid Id { get; } = Guid.NewGuid();
        public ImportingTaskDescriptor Descriptor { get; }
        public IFeatureInfo Feature { get; }
        public MetaEntity Entity { get; }
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();
        public EntityMapping EntityBinding { get; }
        public IFileInfo ImportFileInfo { get; }

        public ImportingTask(
            ImportingTaskDescriptor task,
            IFeatureInfo feature,
            MetaEntity entity,
            EntityMapping entityBinding,
            IFileInfo importFileInfo) {
            this.Descriptor = task ?? throw new ArgumentNullException(nameof(task));
            this.Feature = feature ?? throw new ArgumentNullException(nameof(entity));
            this.Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            this.EntityBinding = entityBinding ?? throw new ArgumentNullException(nameof(entity));
            this.ImportFileInfo = importFileInfo ?? throw new ArgumentNullException(nameof(importFileInfo));
        }
    }

}
