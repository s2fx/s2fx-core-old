using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Data.Importing.Schemas {
    public abstract class AbstractFileDataSourceDefinition : AbstractDataSourceDefinition {
        public string File { get; set; }
    }
}
