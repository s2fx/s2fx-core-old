using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.View.Schemas {

    [ContentProperty(nameof(ViewName))]
    public class ViewActionItem {
        [Required]
        public string ViewName { get; set; }
    }

}
