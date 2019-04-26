using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileProviders;
using OrchardCore.Environment.Extensions.Features;
using S2fx.Model.Metadata;

namespace S2fx.Data.Importing.Model {

    public class ImportingJob {
        public Guid Id { get; } = Guid.NewGuid();
        public bool IsSudo { get; }
        public ImportingJobDescriptor Descriptor { get; }
        public IFeatureInfo Feature { get; }
        public MetaEntity Entity { get; }
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();
        public IFileInfo ImportFileInfo { get; }

        public ImportingJob(
            ImportingJobDescriptor descriptor,
            IFeatureInfo feature,
            MetaEntity entity,
            IFileInfo importFileInfo) {
            this.Descriptor = descriptor ?? throw new ArgumentNullException(nameof(descriptor));
            this.IsSudo = this.Descriptor.IsSudo;
            this.Feature = feature ?? throw new ArgumentNullException(nameof(entity));
            this.Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            this.ImportFileInfo = importFileInfo ?? throw new ArgumentNullException(nameof(importFileInfo));
        }
    }

}
