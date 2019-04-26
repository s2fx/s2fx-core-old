using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.View.Schemas {

    [ContentProperty(nameof(Name))]
    public class Field : VisualElement, IEntityListViewDefinitionWidget {
        public const string TypeName = "Field";

        public string Name { get; set; }
    }

}
