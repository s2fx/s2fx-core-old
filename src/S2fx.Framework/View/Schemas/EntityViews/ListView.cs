using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.View.Schemas {

    [ContentProperty(nameof(Columns))]
    public class ListView : AbstractEntityViewDefinition {
        public override string ViewType => "ListView";

        public ICollection<IEntityListViewDefinitionColumn> Columns { get; } = new List<IEntityListViewDefinitionColumn>();
    }

}
