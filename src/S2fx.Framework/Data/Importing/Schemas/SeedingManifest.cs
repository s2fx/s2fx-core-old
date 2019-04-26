using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.Data.Importing.Schemas {

    [ContentProperty(nameof(InitData))]
    public class SeedingManifest {

        public List<AbstractFileDataSourceDefinition> InitData { get; } = new List<AbstractFileDataSourceDefinition>();

        public List<AbstractFileDataSourceDefinition> DemoData { get; } = new List<AbstractFileDataSourceDefinition>();

    }
}
