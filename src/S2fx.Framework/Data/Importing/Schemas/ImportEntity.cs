using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.Data.Importing.Schemas {

    [ContentProperty(nameof(Fields))]
    public class ImportEntity : AbstractMapping {
        public string Entity { get; set; }
        public List<FieldMapping> Fields { get; } = new List<FieldMapping>();
    }

}
