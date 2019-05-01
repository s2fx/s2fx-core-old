using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Portable.Xaml.Markup;

namespace S2fx.View.Schemas {


    [ContentProperty(nameof(Items))]
    public class ViewAction : AbstractActionDefinition {
        public override string ActionType => "ViewAction";

        public bool CanBeHome { get; set; } = false;

        [Required]
        public List<ActionView> Items { get; } = new List<ActionView>();
    }

}
