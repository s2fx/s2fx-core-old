using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.Data.Importing.Schemas {

    [ContentProperty(nameof(Mappings))]
    public abstract class AbstractDataSourceDefinition : IDataSourceDescriptor {
        public abstract string Format { get; }

        public List<ImportEntity> Mappings { get; } = new List<ImportEntity>();
    }

}
