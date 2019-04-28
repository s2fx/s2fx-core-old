using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.View.Schemas {

    [ContentProperty(nameof(View))]
    public class ActionView {
        [Required]
        public string View { get; set; }
    }

}
