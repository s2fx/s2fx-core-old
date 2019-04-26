using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace S2fx.View.Schemas {

    public abstract class AbstractActionDefinition : Element, IViewDefinition {

        public abstract string ActionType { get; }

        [Required]
        public string Entity { get; set; }

        [Required]
        public string Name { get; set; }

        public string Feature { get; set; } = null;

        [Required]
        public string Text { get; set; }
    }

}
