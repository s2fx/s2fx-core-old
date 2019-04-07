using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.View.Schemas {

    [ContentProperty(nameof(Items))]
    public class S2ViewDefinition {
        public List<INamedElement> Items { get; } = new List<INamedElement>();
    }

}
