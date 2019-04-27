using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.View.Schemas {

    [ContentProperty(nameof(Content))]
    public class ListView : AbstractEntityViewDefinition {
        public override string ViewType => "ListView";

        public ICollection<IEntityListViewDefinitionWidget> Content { get; } = new List<IEntityListViewDefinitionWidget>();
    }

}
