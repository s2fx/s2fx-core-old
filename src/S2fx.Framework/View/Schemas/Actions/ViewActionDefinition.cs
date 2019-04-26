using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.View.Schemas {


    [ContentProperty(nameof(Items))]
    public class ViewActionDefinition : AbstractActionDefinition {
        public override string ActionType => "ViewAction";

        [Required]
        public List<ActionView> Items { get; } = new List<ActionView>();
    }

}
