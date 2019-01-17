using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.UI.Schemas {

    public class MenuEntry {
        public string Name { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public string Icon { get; set; }
        public string BackgroundIcon { get; set; }
        public string Parent { get; set; }
        public string Action { get; set; }
    }

}
