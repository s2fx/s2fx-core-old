using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.Data.Importing.Schemas {

    [ContentProperty(nameof(Children))]
    public class FieldMapping : AbstractMapping {
        public string Field { get; set; }
        public string Format { get; set; }

        public List<FieldMapping> Children { get; } = new List<FieldMapping>();
    }

}
