using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.View.Schemas {

    public class AbstractFragmentDefinition : Element, IViewDefinition {
        public string Feature { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public string Title { get; set; }
        public string ViewType { get; set; }
    }

}
