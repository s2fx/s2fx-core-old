using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.View.Schemas {

    public class Fragment : Element, IViewDefinition {
        public string Feature { get; set; }
        public string Name { get; set; }

    }

}
