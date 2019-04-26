using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.View.Schemas {

    [ContentProperty(nameof(Definition))]
    public abstract class AbstractEntityViewDefinition : VisualElement, IViewDefinition {

        public abstract string ViewType { get; }

        public string Name { get; set; }

        public string Feature { get; set; } = null;

        public string Title { get; set; }

        public string Entity { get; set; }

        public int Priority { get; set; } = 0;

        public string Definition { get; set; }

    }

}
