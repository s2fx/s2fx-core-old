using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.View.Schemas {

    [ViewElement(TypeName)]
    [ContentProperty(nameof(Name))]
    public class Field : VisualElement {
        public const string TypeName = "Field";

        public string Name { get; set; }
    }

}
