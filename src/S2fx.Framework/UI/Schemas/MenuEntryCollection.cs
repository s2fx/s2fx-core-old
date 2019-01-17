using System;
using System.Collections.Generic;
using System.Text;

namespace S2fx.UI.Schemas {

    public class MenuEntryCollection {
        public IList<MenuEntry> Entries { get; private set; } = new List<MenuEntry>();
    }

}
