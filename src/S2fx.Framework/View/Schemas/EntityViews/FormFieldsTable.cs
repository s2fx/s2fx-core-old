using System;
using System.Collections.Generic;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.View.Schemas {

    [ContentProperty(nameof(Fields))]
    public class FormFieldsTable : Layout {

        public int ColumnCount { get; set; }
        public int RowCount { get; set; }

        public int TableRowCount { get; set; }
        public int TableColumnCount { get; set; }

        public List<Field> Fields { get; } = new List<Field>();

    }

}
