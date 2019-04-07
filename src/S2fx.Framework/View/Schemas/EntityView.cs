using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.View.Schemas {

    public abstract class EntityView : VisualElement, INamedElement {

        public string Name { get; set; }

        public string Title { get; set; }

        public string Entity { get; set; }

    }

}
