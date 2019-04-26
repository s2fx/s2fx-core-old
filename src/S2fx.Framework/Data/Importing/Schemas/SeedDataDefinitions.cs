using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.Data.Importing.Schemas {

    [ContentProperty(nameof(DataSources))]
    public class SeedDataDefinitions {

        public List<AbstractDataSourceDefinition> DataSources { get; } = new List<AbstractDataSourceDefinition>();

    }

}
