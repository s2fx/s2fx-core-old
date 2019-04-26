using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.Data.Importing.Schemas {

    public abstract class AbstractMapping {
        public string Selector { get; set; }
        public string Where { get; set; }
        public bool CanCreate { get; set; } = true;
        public bool CanUpdate { get; set; } = true;
    }

}
