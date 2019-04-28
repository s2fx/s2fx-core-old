using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.View.Schemas {

    [ContentProperty(nameof(Definitions))]
    public class S2ViewDefinitions {
        public List<IViewDefinition> Definitions { get; } = new List<IViewDefinition>();
    }

}
