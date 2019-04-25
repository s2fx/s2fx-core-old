using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.View.Schemas {

    public class MenuItemDefinition : Element, IViewDefinition {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Parent { get; set; }
        public int Order { get; set; }
        public string Icon { get; set; }
        public string BackgroundColor { get; set; }
    }

}
