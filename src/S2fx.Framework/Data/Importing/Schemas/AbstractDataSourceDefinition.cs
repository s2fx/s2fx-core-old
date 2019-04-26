using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.Data.Importing.Schemas {

    [ContentProperty(nameof(Mappings))]
    public class AbstractDataSourceDefinition {
        public List<ImportEntity> Mappings { get; } = new List<ImportEntity>();
    }

}
