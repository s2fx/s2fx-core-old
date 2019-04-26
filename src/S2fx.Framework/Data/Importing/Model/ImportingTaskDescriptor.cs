using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Extensions.FileProviders;
using S2fx.Data.Importing.Schemas;

namespace S2fx.Data.Importing.Model {

    public class ImportingTaskDescriptor {
        public bool IsSudo { get; }
        public string Feature { get; }
        public AbstractFileDataSourceDefinition DataSource { get; }
        public ImportEntity ImportEntity { get; }

        public ImportingTaskDescriptor(bool isSudo, string feature, AbstractFileDataSourceDefinition ds, ImportEntity importEntity) {
            this.IsSudo = isSudo;
            this.Feature = feature;
            this.DataSource = ds;
            this.ImportEntity = importEntity;
        }
    }

}
