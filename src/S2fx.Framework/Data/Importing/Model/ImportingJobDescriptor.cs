using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Extensions.FileProviders;
using OrchardCore.Environment.Extensions.Features;
using S2fx.Data.Importing.Schemas;

namespace S2fx.Data.Importing.Model {

    public class ImportingJobDescriptor {
        public bool IsSudo { get; }
        public IFeatureInfo Feature { get; }
        public AbstractFileDataSourceDefinition DataSource { get; }
        public ImportEntity ImportEntity { get; }

        public ImportingJobDescriptor(bool isSudo, IFeatureInfo feature, AbstractFileDataSourceDefinition ds, ImportEntity importEntity) {
            this.IsSudo = isSudo;
            this.Feature = feature;
            this.DataSource = ds;
            this.ImportEntity = importEntity;
        }
    }

}
