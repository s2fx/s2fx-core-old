using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.View.Schemas {


    [ContentProperty(nameof(Items))]
    public class ViewAction : AbstractAction {
        [Required]
        public string Entity { get; set; }

        [Required]
        public List<ViewActionItem> Items { get; } = new List<ViewActionItem>();
    }

}
