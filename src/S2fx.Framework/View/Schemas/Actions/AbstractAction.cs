using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace S2fx.View.Schemas {

    public abstract class AbstractAction : Element, INamedElement {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }
    }

}
