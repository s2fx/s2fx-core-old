using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace S2fx.View.Schemas {

    public abstract class VisualElement : Element {

        [DefaultValue(true)]
        public bool IsVisible { get; set; } = true;

    }

}

