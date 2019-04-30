using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.Data.Importing.Schemas {

    [ContentProperty(nameof(DataSources))]
    public class SeedManifest {

        public List<AbstractFileDataSourceDefinition> DataSources { get; } = new List<AbstractFileDataSourceDefinition>();
    }
}
