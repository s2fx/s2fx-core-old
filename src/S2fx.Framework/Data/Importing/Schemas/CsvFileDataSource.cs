using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Importing.Schemas {
    public class CsvFileDataSource : AbstractFileDataSourceDefinition {
        public override string Format => "CSV";
        public bool HasHeader { get; set; } = true;
    }
}
