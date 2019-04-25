using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.View.Schemas {

    public class MenuItem {
        public string Name { get; set; }
        public string Text { get; set; }
        public MenuItem Parent { get; set; }
        public string Icon { get; set; }
        public string BackgroundColor { get; set; }
        public IReadOnlyList<MenuItem> Children { get; set; }
    }

}
